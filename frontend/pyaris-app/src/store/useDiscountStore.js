import { create } from 'zustand';

export const useDiscountStore = create((set, get) => ({
  discountAmount: 0,
  discountSource: null, // 'Redeem' | 'GiftCard'
  redeemPoints: 0,
  giftCards: [],

  applyRedeem(points, percent) {
    const amount = (points * percent) / 100;
    set({
      discountAmount: amount,
      discountSource: 'Redeem',
      redeemPoints: points,
      giftCards: []
    });
  },

  applyGiftCard(card) {
    const cards = [...get().giftCards, card];
    const total = cards.reduce((s, c) => s + c.value, 0);
    set({
      giftCards: cards,
      discountAmount: total,
      discountSource: 'GiftCard',
      redeemPoints: 0
    });
  },

  removeGiftCard(cardNo) {
    const cards = get().giftCards.filter(c => c.cardNumber !== cardNo);
    const total = cards.reduce((s, c) => s + c.value, 0);
    set({
      giftCards: cards,
      discountAmount: total,
      discountSource: cards.length ? 'GiftCard' : null
    });
  },

  clearDiscount() {
    set({
      discountAmount: 0,
      discountSource: null,
      redeemPoints: 0,
      giftCards: []
    });
  }
}));
