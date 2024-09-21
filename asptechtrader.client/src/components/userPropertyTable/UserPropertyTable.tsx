import { Skeleton, Stack } from "@mui/material";
import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import toast from "react-hot-toast";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../../GlobalRedux/store";
import { updateCurrentSelectedTableSymbol } from "../../GlobalRedux/Features/tableSymbols/tableSymbols-slice";
import { useGetCurrentUser } from "../../features/reactQueryUser/useGetCurrentUser";

const columns: GridColDef[] = [
  {
    field: "symbolName",
    // headerClassName: 'text-[#185875] bg-[#b1b6be] dark:bg-[#1f2739]',
    headerName: "نماد",
    // width: 80,
  },
  {
    field: "volume",
    headerName: "حجم",
    type: "number",
    align: "left",
    headerAlign: "left",
    width: 100,
  },

  {
    field: "lastPrice",
    headerName: "ق پایانی",
    type: "number",
    align: "left",
    headerAlign: "left",
    width: 100,
  },
  {
    field: "lastPricePercentage",
    headerName: "%ق پایانی",
    type: "number",
    align: "left",
    headerAlign: "left",
    width: 100,
  },
  {
    field: "state",
    headerName: "وضعیت",
    type: "number",
    align: "left",
    headerAlign: "left",
    width: 100,
  },
];


export default function UserPropertyTable() {
    const dispatch = useDispatch<AppDispatch>();
    const { currentUser, isLoadingUser, error } = useGetCurrentUser()


    // if is loading return a skeleton
    if (isLoadingUser)
    return (
      <Stack paddingTop={1} spacing={1}>
        <Skeleton
          animation="wave"
          variant="rounded"
          width="full"
          height={140}
        />
      </Stack>
    );

  // if there was an error then retune a toast
  if (error) return toast.error("اخطار.لطفا اتصال اینترنت خود را چک کنید.");

    const rows = currentUser?.userSymbolProperties.map((userSymbolProperty) => {
        return {
            id: userSymbolProperty.symbolId,
            symbolName: userSymbolProperty.symbol.symbolName,
            volume: userSymbolProperty.symbolQuantity,
            lastPrice: userSymbolProperty.symbolPrice,
            lastPricePercentage: `${userSymbolProperty.symbol.lastPricePercentage}%`,
            state: userSymbolProperty.symbol.state === "ALLOWED" ? "مجاز" : "ممنوع",
    };
  });

  // this function is for finding the selected symbol and give it to the redux
    function handleRowSelectionClick(currentSymbolId: string) {
        console.log(currentSymbolId)
      // find the current-selected-symbol from the table and return it
      const currentSelectedUserSymbolProperty = currentUser?.userSymbolProperties!.find((userSymbolProperty: any) => {
          return userSymbolProperty.symbolId == currentSymbolId
      });
      dispatch(updateCurrentSelectedTableSymbol(currentSelectedUserSymbolProperty.symbol!));
  }

  return (
    <Box
      sx={{
        height: { xs: 360, md: 260 },
        bgcolor: "ternery.main",
        scrollbarColor: "blue",
      }}
    >
      <DataGrid
        sx={{ height: { xs: 260, md: 260 } }}
        loading={isLoadingUser}
        scrollbarSize={10}
        columnHeaderHeight={40}
        onRowClick={(event) => handleRowSelectionClick(event.row.id)}
        rowHeight={35}
        rows={rows!}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 15,
            },
          },
        }}
        pageSizeOptions={[15]}
        // checkboxSelection
        // disableRowSelectionOnClick
      />
    </Box>
  );
}
