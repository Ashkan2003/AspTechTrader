import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { Symbol } from "../../types/types";

// this is a custom-hook for fetching the symbols from db with reactQuery and axios
export const useSymbols = () => {
  const {
    data: dataBaseSybmols,
    isLoading,
      error,
  } = useQuery<Symbol[]>({
    queryKey: ["symbols"], // the queryKey is a unic key to identify the data in the cash
    queryFn: async () =>
        await axios.get("https://localhost:7007/api/Symbols/getSymbols").then((res) => res.data), // we pass a function to this to fetch the data
    
  });
  return { dataBaseSybmols, isLoading, error };
};