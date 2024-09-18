import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import toast from "react-hot-toast";

// this is a custom-hook for setting up react-query for creating a single-watchList
export const useCreateWatchList = () => {

  const queryClient = useQueryClient();

  const { mutate } = useMutation({
      mutationFn: (data:{ userWatchListName: string, userId: string }) => {
          return axios({
              method: "post",
              url:"https://localhost:7007/api/UserWatchLists/AddNewWatchList",
              headers: {
                  // Authorization: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmZjU2ZTI4NC00ZjA4LTRhYTktNzE2OC0wOGRjZDQ4MzBmMzUiLCJqdGkiOiJmMjIxZGFjMi1mZTczLTQ2MTQtOWFiOS1iMGU5NDI1NzAwYTUiLCJpYXQiOiI5LzE0LzIwMjQgNjowNDoyMSBBTSIsImV4cCI6MTcyNjI5NDQ2MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAwNyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUxNzMifQ.9qXQ21F53fHkXEgGfYRwRSJyKUg10Gn-sfjB5M4rTxM"
                  Authorization: `Bearer ${localStorage.getItem("token")}`
              },
              data: data
          });
    },
    onSuccess: () => {
      toast.success("دیده بان جدید با موفقیت ایجاد شد.");
      queryClient.invalidateQueries({queryKey:["watchLists"]})
    },
    onError: (error) => {
      toast.error("خطایی رخ داد.");
      console.log(error);
    },
  });
  return { mutate };
};
