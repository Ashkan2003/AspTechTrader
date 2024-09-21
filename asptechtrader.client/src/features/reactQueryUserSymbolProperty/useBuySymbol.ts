import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import toast from "react-hot-toast";

// this is a custom-hook for setting up react-query for deleting a single-watchList
export const useBuySymbol = () => {
  const queryClient = useQueryClient();

  const { mutate } = useMutation({
      mutationFn: (data: {
          symbolPrice: number,
          symbolQuantity: number,
          userId: string,
          symbolId: string
      }) => { 
      // becuse the endpoint-request is "upsert" so we used "put" inisted of the "patch"
      return axios({
              method: "post",
              url:"https://localhost:7007/api/UserSymbolProperty/AddNewBoughtSymbol",
              headers: {
                  Authorization: `Bearer ${localStorage.getItem("token")}`
              },
              data: data
      });
    },
    onSuccess: () => {
      toast.success("سفارش خرید شما با موفقیت ثبت شد.");
      queryClient.invalidateQueries({ queryKey: ["current-user"] });
      queryClient.invalidateQueries({ queryKey: ["symbols"] });
    },
    onError: (error) => {
      toast.error("در ثبت سفارش خطایی رخ داد.");
      console.log(error);
    },
  });
  return { mutate };
};
