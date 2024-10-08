import { Box } from "@mui/material";
import MainColHeader from "../components/mainColumn/MainColHeader";
import MainTable from "../components/mainColumn/MainTable";
import Navbar from "../components/navBar/Navbar";
import SymbolInfo from "../components/symbolInfo/SymbolInfo";
import UserPropertyTable from "../components/userPropertyTable/UserPropertyTable";
import WatchTabs from "../components/watchTabs/WatchTabs";
import DashbordVerticalList from "../ui/DashbordVerticalList";

function Home() {
    return (
        <>
            <Navbar />
            <Box bgcolor="lemon.main" className="flex">
                <div className="grid flex-grow grid-cols-12 grid-rows-5 grid-flow-col  gap-2 mt-2 me-2 ms-2">
                    <div className="hidden sm:block col-span-3 row-span-5 space-y-2">
                        <WatchTabs />
                        <SymbolInfo />
                    </div>
                    <div className="col-span-12 sm:col-span-9 row-span-3">
                        <MainColHeader />
                        <MainTable />
                    </div>
                    <div className="col-span-12 sm:col-span-9 row-span-2 ">
                        <UserPropertyTable />
                    </div>
                </div>
                <div className="hidden sm:block h-[689px] w-[53px]  bg-[#46566E] dark:bg-[#243740]">
                    <DashbordVerticalList />
                </div>
            </Box>
        </>
    );
}

export default Home;