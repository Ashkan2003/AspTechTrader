import { Skeleton, Stack } from "@mui/material";
import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import toast from "react-hot-toast";
import { useGetAllUsers } from "../../features/reactQueryUser/useGetAllUsers";

const columns: GridColDef[] = [
    {
        field: "userName",
        // headerClassName: 'text-[#185875] bg-[#b1b6be] dark:bg-[#1f2739]',
        headerName: "نام کاربر",
        // width: 80,
    },
    {
        field: "emailAddress",
        headerName: "ادرس ایمیل کاربر",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 190,
    },
    {
        field: "userProperty",
        headerName: "دارایی کاربر",
        type: "number",
        align: "center",
        headerAlign: "center",
        width: 100,
    },
    {
        field: "userRole",
        headerName: "سطح دسترسی کاربر",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 150,
    },
    {
        field: "userSymbolPropertyCount",
        headerName: "تعداد نماد  کاربر",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 130,
        valueFormatter: (e) => (`${e.value} نماد`),
    },
    {
        field: "userWatchListCount",
        headerName: "تعداد دیدبان های کاربر",
        type: "string",
        align: "center",
        headerAlign: "center",
        width: 150,
        valueFormatter: (e) => (`${e.value} دیدبان`),
    },

   
];

interface Props {
    setSelectedUser:any
}

export default function AdminMainTable({setSelectedUser }:Props) {

    const { allUsers, isLoadingAllUsers, error } = useGetAllUsers()

    // if is loading return a skeleton
    if (isLoadingAllUsers)
        return (
            <Stack paddingTop={1} spacing={1}>
                <Skeleton animation="wave" variant="rounded" width="full" height={40} />
                <Skeleton
                    animation="wave"
                    variant="rounded"
                    width="full"
                    height={400}
                />
            </Stack>
        );

    // if there was an error then retune a toast
    if (error) return toast.error("اخطار.لطفا اتصال اینترنت خود را چک کنید.");

    // get rows value from user
    const rows = allUsers?.map((user) => {
        return {
            id: user.userId,
            userName: user.userName,
            emailAddress: user.emailAddress,
            userProperty: user.userProperty,
            userRole: user.userRole == 0 ? "کاربر معمولی" : "ادمین",
            userSymbolPropertyCount: user.userSymbolProperties.length,
            userWatchListCount: user.userWatchLists.length
        };
    });

    // this function is for finding the selected symbol and give it to the redux
    function handleRowSelectionClick(currentSelectedUserId:string) {
        // find the current-selected-symbol from the table and return it
        const currentSelectedUser = allUsers?.find((user) => {
            return user.userId == currentSelectedUserId
        });
        setSelectedUser(currentSelectedUser)
    }


    return (
        <Box sx={{ height: 310, bgcolor: "ternery.main", scrollbarColor: "blue" }}>
            <DataGrid
                loading={isLoadingAllUsers}
                scrollbarSize={10}
                columnHeaderHeight={40}
                onRowClick={(event) => handleRowSelectionClick(event.row.id)}
                rowHeight={35}
                rows={rows!}
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

































































//import * as React from 'react';
//import Box from '@mui/material/Box';
//import Collapse from '@mui/material/Collapse';
//import IconButton from '@mui/material/IconButton';
//import Table from '@mui/material/Table';
//import TableBody from '@mui/material/TableBody';
//import TableCell from '@mui/material/TableCell';
//import TableContainer from '@mui/material/TableContainer';
//import TableHead from '@mui/material/TableHead';
//import TableRow from '@mui/material/TableRow';
//import Typography from '@mui/material/Typography';
//import Paper from '@mui/material/Paper';
//import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
//import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

//function createData(
//    name: string,
//    calories: number,
//    fat: number,
//    carbs: number,
//    protein: number,
//    price: number,
//) {
//    return {
//        name,
//        calories,
//        fat,
//        carbs,
//        protein,
//        price,
//        history: [
//            {
//                date: '2020-01-05',
//                customerId: '11091700',
//                amount: 3,
//            },
//            {
//                date: '2020-01-02',
//                customerId: 'Anonymous',
//                amount: 1,
//            },
//        ],
//    };
//}

//function Row(props: { row: ReturnType<typeof createData> }) {
//    const { row } = props;
//    const [open, setOpen] = React.useState(false);

//    return (
//        <React.Fragment>
//            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
//                <TableCell>
//                    <IconButton
//                        aria-label="expand row"
//                        size="small"
//                        onClick={() => setOpen(!open)}
//                    >
//                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
//                    </IconButton>
//                </TableCell>
//                <TableCell component="th" scope="row">
//                    {row.name}
//                </TableCell>
//                <TableCell align="right">{row.calories}</TableCell>
//                <TableCell align="right">{row.fat}</TableCell>
//                <TableCell align="right">{row.carbs}</TableCell>
//                <TableCell align="right">{row.protein}</TableCell>
//            </TableRow>
//            <TableRow>
//                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
//                    <Collapse in={open} timeout="auto" unmountOnExit>
//                        <Box sx={{ margin: 1 }}>
//                            <Typography variant="h6" gutterBottom component="div">
//                                History
//                            </Typography>
//                            <Table size="small" aria-label="purchases">
//                                <TableHead>
//                                    <TableRow>
//                                        <TableCell>Date</TableCell>
//                                        <TableCell>Customer</TableCell>
//                                        <TableCell align="right">Amount</TableCell>
//                                        <TableCell align="right">Total price ($)</TableCell>
//                                    </TableRow>
//                                </TableHead>
//                                <TableBody>
//                                    {row.history.map((historyRow) => (
//                                        <TableRow key={historyRow.date}>
//                                            <TableCell component="th" scope="row">
//                                                {historyRow.date}
//                                            </TableCell>
//                                            <TableCell>{historyRow.customerId}</TableCell>
//                                            <TableCell align="right">{historyRow.amount}</TableCell>
//                                            <TableCell align="right">
//                                                {Math.round(historyRow.amount * row.price * 100) / 100}
//                                            </TableCell>
//                                        </TableRow>
//                                    ))}
//                                </TableBody>
//                            </Table>
//                        </Box>
//                    </Collapse>
//                </TableCell>
//            </TableRow>
//        </React.Fragment>
//    );
//}
//const rows = [
//    createData('Frozen yoghurt', 1592, 6.0, 24, 4.0, 3.99),
//    createData('Ice cream sandwich', 237, 9.0, 37, 4.3, 4.99),
//    createData('Eclair', 262, 16.0, 24, 6.0, 3.79),
//    createData('Cupcake', 305, 3.7, 67, 4.3, 2.5),
//    createData('Gingerbread', 356, 16.0, 49, 3.9, 1.5),
//];
//export default function AdminMainTable() {
//    return (
//        <TableContainer component={Paper} >
//            <Table aria-label="collapsible table">
//                <TableHead>
//                    <TableRow>
//                        <TableCell />
//                        <TableCell>نام کاربر</TableCell>
//                        <TableCell align="right">ادرس ایمیل</TableCell>
//                        <TableCell align="right">دارایی کاربر</TableCell>
//                        <TableCell align="right">Carbs&nbsp;(g)</TableCell>
//                        <TableCell align="right">Protein&nbsp;(g)</TableCell>
//                    </TableRow>
//                </TableHead>
//                <TableBody>
//                    {rows.map((row) => (
//                        <Row key={row.name} row={row} />
//                    ))}
//                </TableBody>
//            </Table>
//        </TableContainer>
//    );
//}
