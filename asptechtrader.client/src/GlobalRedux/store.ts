import { configureStore } from "@reduxjs/toolkit";
import darkModeReducer from "./Features/darkMode/darkMode-slice";
import tableSymbolsReducer from "./Features/tableSymbols/tableSymbols-slice";
import { TypedUseSelectorHook, useSelector } from "react-redux";

export const store = () => {
  return configureStore({
    reducer: {
      darkModeReducer,
      tableSymbolsReducer,
    },
  });
};

export type AppStore = ReturnType<typeof store>;
export const useAppSelectore: TypedUseSelectorHook<RootState> = useSelector;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
