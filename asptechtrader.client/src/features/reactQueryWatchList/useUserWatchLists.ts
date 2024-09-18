import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { userWatchListsType } from "../../types/types";

// this is a custom-hook for fetching the current-user-watchLists from db with reactQuery and axios
export const useUserWatchLists = (userId:string) => {

  const {
    data: user,
    isLoading,
    error,
  } = useQuery({
    
    queryKey: ["watchLists"], // the queryKey is a unic key to identify the data in the cash
    queryFn: async () =>
        await axios({
            method:"get",
            url: `https://localhost:7007/api/UserWatchLists/GetUserWithRelatedUserWatchListById?userId=${userId}`,
            headers: {
                // Authorization: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmZjU2ZTI4NC00ZjA4LTRhYTktNzE2OC0wOGRjZDQ4MzBmMzUiLCJqdGkiOiJmMjIxZGFjMi1mZTczLTQ2MTQtOWFiOS1iMGU5NDI1NzAwYTUiLCJpYXQiOiI5LzE0LzIwMjQgNjowNDoyMSBBTSIsImV4cCI6MTcyNjI5NDQ2MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAwNyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUxNzMifQ.9qXQ21F53fHkXEgGfYRwRSJyKUg10Gn-sfjB5M4rTxM"
                Authorization: `Bearer ${localStorage.getItem("token")}`
            }
        }).then(res=>res.data), // we pass a function to this to fetch the data
  });

    const userWatchList: userWatchListsType[] = user?.userWatchLists

    return { userWatchList, isLoading, error };
};
