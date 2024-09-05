import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { userTradeAccountType } from "../../types/types";

// type userTradeAccountType = TradeAccount & UserBoughtSymbol[];



// this is a custom-hook for fetching the current-user-tradeAccount from db with reactQuery and axios
export const useUserTradeAccount = () => {
  const {
    data: userTradeAccount, // this data includes all the user data-information. like User-model, UserTradeAccount, UserBoughtSymbol-model
    isLoading: isLoadingTradeAccount,
    error,
  } = useQuery<userTradeAccountType>({
    queryKey: ["userTradeAccount"], // the queryKey is a unic key to identify the data in the cash
    queryFn: async () =>
      await axios.get("/api/currentUserTradeAccount").then((res) => res.data), // we pass a function to this to fetch the data
  });
  return { userTradeAccount, isLoadingTradeAccount, error };
};