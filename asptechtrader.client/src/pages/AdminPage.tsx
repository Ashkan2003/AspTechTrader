import { Box, Divider, Paper, Typography } from "@mui/material";
import { useGetCurrentUser } from "../features/reactQueryUser/useGetCurrentUser";
import AdminMainTable from "../components/adminTable/AdminMainTable";
import Navbar from "../components/navBar/Navbar";
import AdminSelectedUserSymbol from "../components/adminTable/AdminSelectedUserSymbols";
import { UserType } from "../types/types";
import { useState } from "react";
import AdminSelectedUserWatchLists from "../components/adminTable/AdminSelectedUserWatchLists";

function AdminPage() {
    const { isLoadingUser, currentUser } = useGetCurrentUser()
    const [selectedUser, setSelectedUser] = useState<UserType>()

    if (isLoadingUser) return null

    if (currentUser?.userRole != 1) { // if userRole was not admin show error
        return (
            <Box sx={{ height: 633, bgcolor: "ternery.main", padding: "250px" }}>
                <Paper
                    //elevation={3}
                    sx={{
                        textAlign: "center",
                        padding: "50px",
                    }}
                >
                    <Typography>شما اجازه دسترسی به این صفحع را ندارید.</Typography>
                    <Typography fontSize="30px" color="red" paddingTop="20px">403</Typography>

                </Paper>
            </Box>
        )
    }

    return (
        <Box sx={{  bgcolor: "lemon.main" }}>
          {/*header*/}
          <Navbar/>
            {/*adminMainTable*/}
            <div className="p-2">
               <AdminMainTable setSelectedUser={setSelectedUser} />
               <div className="grid grid-col-1 md:grid-cols-2 py-3 gap-3  ">
                  {/*admin-selectedUserSymbolsProperty-table*/}
                  <AdminSelectedUserSymbol selectedUser={selectedUser} />
                  {/*admin-selectedUserWatchList-table*/}
                  <AdminSelectedUserWatchLists selectedUser={selectedUser} />
               </div>
            </div>
      </Box>

  );
}

export default AdminPage;