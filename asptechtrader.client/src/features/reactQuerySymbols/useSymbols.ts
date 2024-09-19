import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { SymbolType } from "../../types/types";

// this is a custom-hook for fetching the symbols from db with reactQuery and axios
export const useSymbols = () => {
  const {
    data: dataBaseSybmols,
    isLoading,
      error,
  } = useQuery<SymbolType[]>({
      queryKey: ["symbols"], // the queryKey is a unic key to identify the data in the cash
      queryFn: async () => axios({
          method: "get",
          url: "https://localhost:7007/api/Symbols/getSymbols",
          headers: {
              Authorization: `Bearer ${localStorage.getItem("token")}`
          }
      }).then(res => res.data)
       
  });
  return { dataBaseSybmols, isLoading, error };
};
