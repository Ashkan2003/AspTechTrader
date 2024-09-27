import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { UserType } from "../../types/types";

const columns: GridColDef[] = [
    {
        field: "userWatchListId",
        // headerClassName: 'text-[#185875] bg-[#b1b6be] dark:bg-[#1f2739]',
        headerName: "نام دیدبان",
        // width: 80,
    },
    {
        field: "userWatchListSymbolsName",
        headerName: "نمادهای دیدبان",
        type: "string",
        align: "left",
        headerAlign: "left",
        width: 190,
    },
   


];

interface Props {
    selectedUser?: UserType
}

export default function AdminSelectedUserWatchLists({ selectedUser }: Props) {


    // get rows value from user // if selecterUser was null so render []
    const rows = selectedUser ?
        selectedUser.userWatchLists.map((userWatchList) => {
            return {
                id: userWatchList.userWatchListId,
                userWatchListId: userWatchList.userWatchListName,
                userWatchListSymbolsName: userWatchList.symbols.map((symbol) => { return symbol.symbolName }).join("-")
            };
        })
        : []


    return (
        <Box sx={{ height:"245px", bgcolor: "ternery.main", scrollbarColor: "blue" }}>
            <DataGrid
                rows={rows}
                scrollbarSize={10}
                columnHeaderHeight={40}
                rowHeight={35}
                columns={columns}
                initialState={{
                    pagination: {
                        paginationModel: {
                            pageSize: 10,
                        },
                    },
                }}
                pageSizeOptions={[10]}

            // checkboxSelection
            // disableRowSelectionOnClick
            />
        </Box>
    );
}









