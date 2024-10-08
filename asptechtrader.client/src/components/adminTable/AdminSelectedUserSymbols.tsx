﻿import Box from "@mui/material/Box";
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
    {
        field: "symolBoughtDate",
        headerName: "تاریخ خرید سهم",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 190,
        //valueFormatter: (e) => (`${e.value} سهم`),
    },
    


];

interface Props {
    selectedUser?: UserType
}

export default function AdminSelectedUserSymbol({ selectedUser }:Props) {
    function generateRandomDate() {
        const randomDayNumber = Math.floor(Math.random() * 30);
        const randomDay = randomDayNumber <= 9 ? `0${randomDayNumber}` : randomDayNumber

        const randomMonthNumber = Math.floor(Math.random() * 30);
        let randomMonth = randomMonthNumber == 0 ? 1 : randomMonthNumber
        randomMonth = randomMonth <= 9 ? `0${randomMonth}` : randomMonth

        return `1403/${randomMonth}/${randomDay}`
    }


    // get rows value from user // if selecterUser was null so render []
    const rows = selectedUser ? 
                   selectedUser.userSymbolProperties?.map((userSymbolProperty) => {
                        return {
                                 id: userSymbolProperty.userSymbolPropertyId,
                                 symbolName: userSymbolProperty.symbol.symbolName,
                                 symbolPrice: userSymbolProperty.symbolPrice,
                                 symbolQuantity: userSymbolProperty.symbolQuantity,
                                 symolBoughtDate: generateRandomDate()
                        };
                   })
                   : []


    return (
        <Box sx={{ height: 320, bgcolor: "ternery.main", scrollbarColor: "blue" }}>
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












