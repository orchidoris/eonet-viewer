import { create } from 'zustand';

interface UIState {
  filtersVisible: boolean;
  toggleFilters: () => void;
}

export const useUIState = create<UIState>((set) => ({
  filtersVisible: true,
  toggleFilters: () => {
    set((state) => ({
      filtersVisible: !state.filtersVisible,
    }));
  },
}));
