import { Box, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { useGetCurrentUser } from "../features/reactQueryUser/useGetCurrentUser";
import AdminMainTable from "../components/adminTable/AdminMainTable";

function AdminPage() {
    const { isLoadingUser, currentUser } = useGetCurrentUser()

    if (isLoadingUser) return null

    if (currentUser?.userRole != 1) { // if userRole was not admin show error
        return (
            <Box sx={{ height: 633, bgcolor: "ternery.main" }}>
                <Paper
                    elevation={3}
                    sx={{
                        textAlign: "center",
                        padding: "50px",
                    }}
                >
                    <Typography>شما اجازه دسترسی به این صفحع را ندارید.</Typography>
                </Paper>
            </Box>
        )
    }

  return (
      <Box sx={{ height: 633, bgcolor: "ternery.main" }}>
          {/*header*/}
          <header className="bg-[#5D6E88] dark:bg-[#2D3E4A]  h-13">
              <nav className="flex  items-center justify-between  ">
                  <div className="flex  items-center">
                      <div className="flex items-center   pe-3 ">
                         <img src="/Trade-brand.png" alt="brand" width="50" height="30" />
                          <Divider
                              className="hidden sm:block"
                              orientation="vertical"
                              flexItem
                          />
                          <Link to="/" className="px-4">
                              <Typography className="hidden md:block" color="white">
                                  برگشت به خانه
                              </Typography>
                          </Link>
                          <Divider
                              className="hidden sm:block"
                              orientation="vertical"
                              flexItem
                          />
                      </div>    
                  </div>    
              </nav>
          </header>

          {/*adminMainTable*/}
          <AdminMainTable/>
      </Box>

  );
}

export default AdminPage;