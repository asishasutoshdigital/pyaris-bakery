import { useState, useEffect } from 'react';
import { useLocationStore } from '../store/useStore';

function LocationPopup() {
  const [searchTerm, setSearchTerm] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const [tempPincode, setTempPincode] = useState('');
  const [tempArea, setTempArea] = useState('');
  
  const { isPopupOpen, closePopup, setLocation } = useLocationStore();

  // Pincode data (from original ASPX)
  const pincodes = [
    "751001", "751002", "751003", "751004", "751005", "751006", "751007", "751008", "751009", "751010", "751011", "751012", "751013", "751014", "751015", "751016", "751017",
    "751018", "751019", "751020", "751021", "751022", "751023", "751024", "751025", "751026", "751027", "751028", "751030", "753001", "753002", "753003", "753004", "753005", "753007",
    "753008", "753009", "753010", "753012", "753013", "753014", "753015"
  ];

  const pincodeAreas = {
    "751001": ["AG Square", "Bhubaneswar GPO", "Bhubaneswar Secretariat", "C G Colony", "Kharavela Nagar", "Orissa Assembly"],
    "751002": ["Bhimatangi", "Old Town", "Kedargouri", "Sisupalgarh", "Sundarpada", "Kausalyaganga", "Harachandi Sahi", "Bankual", "Ebaranga", "Gopinathpur", "Itipur", "Kalyanpur Sasan", "Kuha", "Santarapur"],
    "751003": ["Baramunda Colony", "Bharatpur", "Ganganagar", "Ghatikia", "Suryanagar", "Andharua", "Malipada"],
    "751004": ["Utkal University"],
    "751005": ["Sainik School"],
    "751006": ["Budheswari Colony", "Kalpana Square", "Laxmisagar", "B.S. Colony", "Jharapada"],
    "751007": ["Saheed Nagar", "Satyanagar", "V S S Nagar"],
    "751008": ["Rajbhawan"],
    "751009": ["Ashok Nagar", "Bapujee Nagar", "Bhubaneswar R S", "Udyan Marg"],
    "751010": ["Gada Gopinath Prasad", "Rasulgarh"],
    "751011": ["C R P Lines"],
    "751012": ["Nayapalli"],
    "751013": ["Regional Research Laboratory (CSIR-IMMT)"],
    "751014": ["B J B Nagar", "Bhubaneswar Court"],
    "751015": ["I R C Village"],
    "751016": ["Chandra Sekhar Pur"],
    "751017": ["Mancheswar", "Mancheswar Railway Colony", "OSAP Campus", "Mancheswar R S"],
    "751018": ["Badagarh Brit Colony"],
    "751019": ["Aerodrome Area", "Aiginia", "AIIMS", "Dumduma Housing Board Colony", "Jadupur", "Kalinga Vihar", "Patrapada", "Sarakantara"],
    "751020": ["Aerodrome Area", "Airport", "Bhim Tangi", "Pokhariput"],
    "751021": ["Sailashree Vihar"],
    "751022": ["Acharya Vihar", "Bhoinagar", "Madhusudan Nagar"],
    "751023": ["S.E Rly.Proj. Complex"],
    "751024": ["K I I T", "Kalarahanga", "Patia Gds"],
    "751025": ["G.G.P.Colony"],
    "751026": ["N.M.D Parcel Centre Bhubaneswar"],
    "751027": ["International Business Centre"],
    "751028": ["Tamando"],
    "751030": ["Khandagiri"],
    "753001": ["Cuttack GPO (General Post Office)", "Bajrakabati Road", "Cantonment Road", "Choudhury Bazar", "Dargha Bazar", "Gandhi Bhawan", "Kazibazar", "Mangalabag", "Meria Bazar", "Nimasahi", "Pattapole", "Buxi Bazaar", "Malgodown", "Shaikh Bazaar", "Ranihat (parts)", "Telenga Bazar", "Balu Bazar", "College Square (parts)", "Jobra (parts)", "Tulsipur (parts)"],
    "753002": ["Binod Bihari", "Chandinichowk", "Dagarpara", "Kafla", "Oriya Bazar", "Pithapur", "Khatbin Sahi"],
    "753003": ["Chhatra Bazar", "College Square", "Jobra", "Kaliaboda", "Nuapatna (parts)", "Bidanasi (parts)", "CDA Sector 1 to 5 (older parts closer to the core)"],
    "753004": ["Chauliaganj", "Sikharpur"],
    "753005": ["Barabati Stadium", "Killa Maidan (Fort Ground)"],
    "753007": ["Ranihat (primarily residential, but distinct from the core commercial part covered by 753001)", "Mahatab Road", "Dolamundai"],
    "753008": ["Bidanasi", "Kanika Rajbati", "Biju Patnaik Colony", "Deulasahi Colony", "CDA Sector 6 (parts)"],
    "753009": ["Nuapatna (older parts, distinct from CDA's newer sectors)", "Naya Bazar (parts)"],
    "753010": ["Industrial Estate (Cuttack)", "Madhupatna", "Government Press Employee Quarters"],
    "753012": ["A D Market", "Badambadi Colony", "Badambadi Bus Stand Area"],
    "753013": ["Kalyani Nagar", "CDA Sector 10 (parts, as it merges into the older town areas)"],
    "753014": ["Avinab Bidanasi", "CDA Area (specifically the well-established sectors like CDA Sec - 1 to 8, 10, etc. that are considered part of the expanded town area)", "Naya Bazar (parts)"],
    "753015": ["CDA Sector 11 (newer, but still considered within the extended urban limits of Cuttack Town due to its proximity and development)"]
  };

  useEffect(() => {
    if (searchTerm.length === 0) {
      setSuggestions([]);
      return;
    }

    const input = searchTerm.toLowerCase();
    const addedSuggestions = new Set();
    const results = [];

    // Match by pincode
    pincodes.forEach(pincode => {
      if (pincode.startsWith(input)) {
        const areas = pincodeAreas[pincode] || ["Unknown Area"];
        areas.forEach(area => {
          const key = `${area}-${pincode}`;
          if (!addedSuggestions.has(key)) {
            results.push({ area, pincode });
            addedSuggestions.add(key);
          }
        });
      }
    });

    // Match by area name
    for (const [pincode, areas] of Object.entries(pincodeAreas)) {
      areas.forEach(area => {
        if (area.toLowerCase().includes(input)) {
          const key = `${area}-${pincode}`;
          if (!addedSuggestions.has(key)) {
            results.push({ area, pincode });
            addedSuggestions.add(key);
          }
        }
      });
    }

    setSuggestions(results);
  }, [searchTerm]);

  const handleSuggestionClick = (area, pincode) => {
    setSearchTerm(`${area} - ${pincode}`);
    setTempPincode(pincode);
    setTempArea(area);
    setSuggestions([]);
  };

  const handleContinue = () => {
    if (tempPincode && tempArea && pincodes.includes(tempPincode)) {
      setLocation(tempArea, tempPincode);
      closePopup();
    } else {
      alert("Please select a valid pincode and area from suggestions.");
    }
  };

  const handleClose = () => {
    closePopup();
  };

  const handleClear = () => {
    setSearchTerm('');
    setTempPincode('');
    setTempArea('');
    setSuggestions([]);
  };

  if (!isPopupOpen) return null;

  return (
    <>
      <div id="popupBg" className="popup-background" style={{display: 'block'}} onClick={handleClose}></div>
      
      <div id="popupDiv" className="popup-container" style={{display: 'block'}}>
        <div className="popup-header">
          Choose Your Delivery Location
          <span className="close-btn" onClick={handleClose}>×</span>
        </div>
        <div className="popup-body">
          <p>
            <span style={{color: '#e91e63'}}>ⓘ</span> Where would you like to get the product delivered?<br/>
          </p>

          <div className="center-screen">
            <div className="input-wrapper">
              <input 
                type="text" 
                id="pincodeInput" 
                className="truncate-text" 
                placeholder="Enter Delivery Pincode"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                autocomplete="off"
              />
              <button type="button" className="clear-btn" onClick={handleClear}>
                <i className="fas fa-times"></i>
              </button>
              {suggestions.length > 0 && (
                <div id="suggestions" className="suggestion-box">
                  {suggestions.map((item, index) => (
                    <div 
                      key={index}
                      className="suggestion-item"
                      onClick={() => handleSuggestionClick(item.area, item.pincode)}
                    >
                      {item.area} - {item.pincode}
                    </div>
                  ))}
                </div>
              )}
              {suggestions.length === 0 && searchTerm.length > 0 && (
                <div id="suggestions" className="suggestion-box">
                  <div className="suggestion-item">No match found</div>
                </div>
              )}
            </div>
          </div>

          <button type="button" className="continue-button" onClick={handleContinue}>CONTINUE</button>
        </div>
      </div>
    </>
  );
}

export default LocationPopup;
