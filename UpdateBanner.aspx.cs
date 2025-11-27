using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Tls.Crypto;
using System.Drawing;

public partial class UpdateBanner : System.Web.UI.Page
{
    private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private const int MaxFileSize = 2 * 1024 * 1024;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fuNewBannerImage.Attributes["accept"] = ".jpg,.jpeg,.png,.gif";
            showCurrentBanners();
            PopulateNewBannerGroupDropdown();
        }
    }

    // --- Helper Method to Bind Banners to Repeater ---
    private void showCurrentBanners()
    {
        try
        {
            string bannerDirectory = Server.MapPath("~/images/slider/");
            if (!Directory.Exists(bannerDirectory))
            {
                Directory.CreateDirectory(bannerDirectory);
            }

            string[] allFiles = Directory.GetFiles(bannerDirectory, "*.*")
                               .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                           f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                           f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                               .ToArray();

            var matchedFiles = allFiles.Where(f => Path.GetFileName(f).IndexOf("Slider-", StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            var bannerFiles = matchedFiles.Select(file => new
                                    {
                                        FileName = Path.GetFileName(file),
                                        Group = GetBannerGroup(Path.GetFileName(file)),
                                        ActiveStatus = GetBannerIsActive(Path.GetFileName(file))
            })
                                    .OrderBy(f => f.FileName)
                                    .ToList();

            rptCurrentBanners.DataSource = bannerFiles;
            rptCurrentBanners.DataBind();
        }
        catch (Exception ex)
        {
            SetToastMessage("Error loading banners: " + ex.Message, "red");
        }
    }

    // --- Repeater ItemData Event (for populating group dropdowns in existing banners) ---
    protected void rptCurrentBanners_ItemData(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            FileUpload fuBannerImage = (FileUpload)e.Item.FindControl("fuBannerImage");
            if (fuBannerImage != null)
            {
                fuBannerImage.Attributes["accept"] = ".jpg,.jpeg,.png,.gif";
            }
            DropDownList ddlGroup = (DropDownList)e.Item.FindControl("ddlGroup");
            if (ddlGroup != null)
            {
                ddlGroup.Items.Clear();
                ddlGroup.Items.Insert(0, new ListItem("-- Select Group --", ""));
                PopulateGroupDropdown(ddlGroup);

                dynamic bannerData = e.Item.DataItem;
                if (bannerData != null && !string.IsNullOrEmpty(bannerData.Group))
                {
                    ListItem itemToSelect = ddlGroup.Items.FindByValue(bannerData.Group);
                    if (itemToSelect != null)
                    {
                        itemToSelect.Selected = true;
                    }
                }
            }
        }
    }

    // --- Show Group Dropdown (for both new and existing banners) ---
    private void PopulateGroupDropdown(DropDownList ddl)
    {
        HashSet<string> uniqueGroupItems = new HashSet<string>();

        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                var cmdx = new SqlCommand("SELECT DISTINCT [Sub Group] FROM [XMaster Menu] WHERE [Active] = 1", cn);

                using (var drx = cmdx.ExecuteReader())
                {
                    while (drx.Read())
                    {
                        string subGroup = drx["Sub Group"].ToString();
                        if (subGroup != "CAKES")
                        {
                            if (subGroup.Contains(','))
                            {
                                string[] items = subGroup.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string item in items)
                                {
                                    uniqueGroupItems.Add(item.Trim());
                                }
                            }
                            else
                            {
                                uniqueGroupItems.Add(subGroup.Trim());
                            }
                        }
                    }
                }
                foreach (string item in uniqueGroupItems.OrderBy(i => i))
                {
                    ddl.Items.Add(new ListItem(item, item));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error populating group dropdown: " + ex.Message);
            }
        }
    }

    // --- Populate dropdown for the "Add New Banner" section ---
    private void PopulateNewBannerGroupDropdown()
    {
        ddlNewBannerGroup.Items.Clear();
        ddlNewBannerGroup.Items.Insert(0, new ListItem("-- Select Group --", ""));
        PopulateGroupDropdown(ddlNewBannerGroup); 
    }

    // --- Helper Method to Get Banner Group from DB (Placeholder) ---
    private string GetBannerGroup(string fileName)
    {
        string group = string.Empty;
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                var cmd = new SqlCommand("SELECT [data] FROM [mastercodes] WHERE [options] = @FileName", cn);
                cmd.Parameters.AddWithValue("@FileName", "images/slider/" + fileName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    group = result.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error retrieving group for {0}: {1}", fileName, ex.Message));
            }
        }
        string originalfile = "images/slider/" + fileName;

        return group;
    }
    private int GetNextSliderNumber()
    {
        string bannerDirectory = Server.MapPath("~/images/slider/");
        if (!Directory.Exists(bannerDirectory))
        {
            return 1; // If directory doesn't exist, start with 1
        }

        int maxNumber = 0;
        Regex regex = new Regex(@"Slider-(\d+)\.(jpg|jpeg|png|gif)$", RegexOptions.IgnoreCase);

        // Filter files that match the "Slider-X.ext" pattern
        var sliderFiles = Directory.GetFiles(bannerDirectory)
                                  .Select(Path.GetFileName)
                                  .Where(f => regex.IsMatch(f));

        foreach (string fileName in sliderFiles)
        {
            Match match = regex.Match(fileName);
            if (match.Success)
            {
                int number = int.Parse(match.Groups[1].Value);
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }
        }
        return maxNumber + 1;
    }
    // --- Handler for Adding New Banners ---
    protected void btnAddNewBanner_Click(object sender, EventArgs e)
    {
        if (!fuNewBannerImage.HasFile)
        {
            SetToastMessage("Please select an image file to upload for the new banner.", "red");
            return;
        }

        if (string.IsNullOrEmpty(ddlNewBannerGroup.SelectedValue))
        {
            SetToastMessage("Please select a group for the new banner.", "red");
            return;
        }

        if (!IsValidFile(fuNewBannerImage))
        {
            return; 
        }

        try
        {
            // --- MODIFIED: Generate new file name ---
            int nextNumber = GetNextSliderNumber();
            string originalExtension = Path.GetExtension(fuNewBannerImage.FileName);
            string newFileName = string.Format("Slider-{0}{1}", nextNumber, originalExtension);

            string savePath = Server.MapPath("~/images/slider/" + newFileName);
            string selectedGroup = ddlNewBannerGroup.SelectedValue;

            fuNewBannerImage.SaveAs(savePath);

            InsertBannerIntoDatabase(newFileName, selectedGroup);

            SetToastMessage("New banner '"+newFileName+"' added successfully and assigned to group '"+ selectedGroup+"'!", "green");
            showCurrentBanners(); 
            fuNewBannerImage.Attributes.Clear();
            ddlNewBannerGroup.SelectedIndex = 0;
            PopulateNewBannerGroupDropdown();
        }
        catch (Exception ex)
        {
            SetToastMessage("Error adding new banner: " + ex.Message, "red");
        }
    }

    // --- Method to Insert New Banner Data into Database ---
    private void InsertBannerIntoDatabase(string fileName, string selectedGroup)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                string query = "insert into [MasterCodes] values ('websiteSlider',@Path,'1',GETDATE(),'ADMIN',GETDATE(),'ADMIN',@group)";
                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Path", "images/slider/" + fileName);
                    cmd.Parameters.AddWithValue("@Group", selectedGroup);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database insert failed: " + ex.Message, ex);
            }
        }
    }

    // --- Method to Update Banner Data in Database ---
    // Add the new isActive param to UpdateBannerInDatabase()
    private void UpdateBannerInDatabase(string originalFileName, string newFileName, string selectedGroup)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                string query = "UPDATE [mastercodes] SET [data] = @Group WHERE [options] = @OriginalPath";
                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Group", selectedGroup);
                    //cmd.Parameters.AddWithValue("@Active", isActive ? "1" : "0");
                    cmd.Parameters.AddWithValue("@OriginalPath", "images/slider/" + originalFileName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database update failed: " + ex.Message, ex);
            }
        }
    }
    private bool isActive;
    // Add this helper to get active status
private bool GetBannerActiveStatus(string fileName)
{
    string connectionString = Program.Connection;
    using (var cn = new SqlConnection(connectionString))
    {
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT [Enabled] FROM [mastercodes] WHERE [options] = @Path", cn);
            cmd.Parameters.AddWithValue("@Path", "images/slider/" + fileName);
            var result = cmd.ExecuteScalar();
            
            if (result != null)
            {
                return bool.TryParse(result.ToString(), out isActive); 
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
            
    }
}
    private string bannerGroupCheckName=string.Empty;
    private void CheckBannerGroup(string fileName)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                var cmd = new SqlCommand("SELECT [Data] FROM [mastercodes] WHERE [options] = @Path", cn);
                cmd.Parameters.AddWithValue("@Path", "images/slider/" + fileName);
                var result = cmd.ExecuteReader();
                if (result.HasRows) 
                { 
                   result.Read();
                    bannerGroupCheckName = result[0].ToString();              
                }
                cn.Close();
            }
            catch(Exception ex) 
            {
                
            }

        }
    }
    private bool GetBannerIsActive(string fileName)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                var cmd = new SqlCommand("SELECT [Enabled] FROM [mastercodes] WHERE [options] = @Path and [Enabled]='1'", cn);
                cmd.Parameters.AddWithValue("@Path", "images/slider/" + fileName);
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    var rs = result.Read();
                    isActive = true;
                    return true;
                }
                isActive = false;
                return false;

            }
            catch
            {
                return false;
            }

        }
    }
    private void UpdateActiveStatus(string fileName,bool isActive)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                string path="images/slider/" + fileName+"";
                string value = "";
                if (isActive == true) {
                    value = "0";               
                }
                else
                {
                    value = "1";
                }
                string query = "UPDATE [mastercodes] SET [Enabled] = '"+value+"' WHERE [options] = '"+path+"'";
                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database update failed: " + ex.Message, ex);
            }
        }
    }

    // Update the ItemCommand to include active checkbox
    protected void rptCurrentBanners_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string originalFileName = e.CommandArgument.ToString();

        if (e.CommandName == "UpdateBanner")
        {
            FileUpload fuBannerImage = (FileUpload)e.Item.FindControl("fuBannerImage");
            DropDownList ddlGroup = (DropDownList)e.Item.FindControl("ddlGroup");

            if (fuBannerImage != null && ddlGroup != null )
            {
                string selectedGroup = ddlGroup.SelectedValue;

                if (string.IsNullOrEmpty(selectedGroup))
                {
                    SetToastMessage("Please select a group for banner '"+originalFileName+"'.", "red");
                    return;
                }

                try
                {
                    bool fileUploaded = false;

                    if (fuBannerImage.HasFile)
                    {
                        if (!IsValidFile(fuBannerImage)) return;

                        string savePath = Server.MapPath("~/images/slider/" + originalFileName);
                        fuBannerImage.SaveAs(savePath);
                        fileUploaded = true;
                    }

                    CheckBannerGroup(originalFileName);
                    UpdateBannerInDatabase(originalFileName, originalFileName, selectedGroup);

                    if (fileUploaded)
                    {
                        SetToastMessage("Banner '"+originalFileName+"' updated and assigned to group '"+selectedGroup+"'.", "green");
                    }
                    else
                    {
                        SetToastMessage("Group for banner '"+originalFileName+"' updated to '"+selectedGroup+"'.", "green");
                    }

                }
                catch (Exception ex)
                {
                    SetToastMessage("Error updating banner '"+originalFileName+"': {ex.Message}", "red");
                }
            }
        }
        else if (e.CommandName == "DeleteBanner")
        {
            try
            {
                string filePath = Server.MapPath("~/images/slider/" + originalFileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);

                DeleteBannerFromDatabase(originalFileName);

                SetToastMessage("Banner '"+originalFileName+"' deleted successfully!", "green");
            }
            catch (Exception ex)
            {
                SetToastMessage("Error deleting banner '"+originalFileName+"': {ex.Message}", "red");
            }
        }
        else if (e.CommandName == "ActiveStatus")
        {
            GetBannerIsActive(originalFileName);
            UpdateActiveStatus(originalFileName, isActive);
            SetToastMessage("The banner status has been updated.", "green");
        }
        showCurrentBanners();
    }

    // --- Method to Delete Banner Data from Database ---
    private void DeleteBannerFromDatabase(string fileName)
    {
        string connectionString = Program.Connection;
        using (var cn = new SqlConnection(connectionString))
        {
            try
            {
                cn.Open();
                string query = "DELETE FROM [mastercodes] WHERE [options] = @Path";
                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Path", "images/slider/" + fileName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database deletion failed: " + ex.Message, ex);
            }
        }
    }

    // --- File Validation Method ---
    private bool IsValidFile(FileUpload fileUploadControl)
    {
        string fileExtension = Path.GetExtension(fileUploadControl.FileName).ToLower();

        if (!AllowedExtensions.Contains(fileExtension))
        {
            SetToastMessage("Invalid file type. Only JPG, PNG, and GIF images are allowed.", "red");
            return false;
        }

        if (fileUploadControl.PostedFile.ContentLength > MaxFileSize)
        {
            SetToastMessage(string.Format("File size too large. Maximum allowed size is {0} MB.", MaxFileSize / (1024 * 1024)), "red");
            return false;
        }

        return true;
    }

    // --- Unified Toast Message Setter ---
    private void SetToastMessage(string message, string Color)
    {
        litMessage.ForeColor = System.Drawing.Color.FromName(Color);
        litMessage.Text = message;
    }
}