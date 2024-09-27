import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { UserType } from "../../types/types";

const columns: GridColDef[] = [
    {
        field: "symbolName",
        // headerClassName: 'text-[#185875] bg-[#b1b6be] dark:bg-[#1f2739]',
        headerName: "نماد",
        // width: 80,
    },
    {
        field: "symbolPrice",
        headerName: "قیمت خریداری شده سهم",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 190,
        valueFormatter: (e) => (`${e.value} ریال`),
    },
    {
        field: "symbolQuantity",
        headerName: "حجم خریداری شده سهم",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 190,
        valueFormatter: (e) => (`${e.value} سهم`),
    },
    


];

interface Props {
    selectedUser?: UserType
}

export default function AdminSelectedUserSymbol({ selectedUser }:Props) {

    // get rows value from user // if selecterUser was null so render []
    const rows = selectedUser ? 
                   selectedUser.userSymbolProperties?.map((userSymbolProperty) => {
                        return {
                                 id: userSymbolProperty.userSymbolPropertyId,
                                 symbolName: userSymbolProperty.symbol.symbolName,
                                 symbolPrice: userSymbolProperty.symbolPrice,
                                 symbolQuantity: userSymbolProperty.symbolQuantity,
                        };
                   })
                   : []


    return (
        <Box sx={{ height: 245, bgcolor: "ternery.main", scrollbarColor: "blue" }}>
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












