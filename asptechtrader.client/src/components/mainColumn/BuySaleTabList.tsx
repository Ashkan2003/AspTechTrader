import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";
import BuyTab from "./BuyTab";
import SaleTab from "./SaleTab";
import { useAppSelectore } from "../../GlobalRedux/store";
import { useGetCurrentUser } from "../../features/reactQueryUser/useGetCurrentUser";
import { UserSymbolProperties } from "../../types/types";

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function CustomTabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ py: 1 }}>{children}</Box>}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

interface Props {
  priceInputValue: number;
  volumeInputValue: number;
  tabListIndexvalue: number;
  setPriceInputValue: React.Dispatch<React.SetStateAction<number>>;
  setVolumeInputValue: React.Dispatch<React.SetStateAction<number>>;
  setTabListIndexvalue: React.Dispatch<React.SetStateAction<number>>;
  handleSetUserBoughtSymbolCountToVulomeInput: any;
  handleClose: unknown;
}

export default function BuySaleTabList({
  priceInputValue,
  volumeInputValue,
  tabListIndexvalue,
  setPriceInputValue,
  setVolumeInputValue,
  setTabListIndexvalue,
  handleSetUserBoughtSymbolCountToVulomeInput,
  handleClose,
}: Props) {
    //get the current-selected-symbol by user from redux
    const currentSymbol = useAppSelectore(
    (state) => state.tableSymbolsReducer.currentSelectedTableSymbol
  );

    // react-query
    const { currentUser, isLoadingUser } = useGetCurrentUser()
   
    if (isLoadingUser) return null;

    // get the symbols that user bought
    const userSymbolProperties = currentUser?.userSymbolProperties;

  // explanation
  // we have an currentSymbol that is the current-selected-symbol by user from the main col that is stored in redux
  // in the other hand, we have a array of symbols that the user bought priviosily
    // so for fatcing this symbol name and count i looped throug the userBoughtSymbols and find it
    const userCurrentBoughtSymbol: UserSymbolProperties | undefined = userSymbolProperties!.find(userSymbolProperty => {
        if (userSymbolProperty.symbolId === currentSymbol?.symbolId)
            return userSymbolProperty;
    }
    );
   

    //get the currentBoughtSymbol symbolID.if its undfind it means that the user dont bought this symbol-previosly,so set it to currentSelectedSymbol.ID
    const currentBoughtSymbolId = userCurrentBoughtSymbol
        ? userCurrentBoughtSymbol.symbolId
        : currentSymbol?.symbolId

  // get the currentBoughtSymbol count.if its undfind it means that the user dont bought this symbol-previosly,so set it 0
    const currentBoughtSymbolQuantity = userCurrentBoughtSymbol
        ? userCurrentBoughtSymbol.symbolQuantity
        : 0;

  // get todey-date in farsi-date
  const todayDate = new Date().toLocaleDateString("fa-IR");

  // this function is for the selection of the tab
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabListIndexvalue(newValue);
  };
  return (
    <Box
      sx={{
        bgcolor: "ternery.main",
        color: "textPallet.main",
        width: "100%",
      }}
    >
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          sx={{ color: "ternary.main" }}
          textColor="inherit"
          indicatorColor="secondary"
          variant="fullWidth"
          centered
          value={tabListIndexvalue}
          onChange={handleChange}
        >
          <Tab label="خرید" sx={{ fontSize: "20px" }} {...a11yProps(0)} />
          <Tab label="فروش" sx={{ fontSize: "20px" }} {...a11yProps(1)} />
        </Tabs>
      </Box>
      <CustomTabPanel value={tabListIndexvalue} index={0}>
        <BuyTab
          currentSymbol={currentSymbol!}
          currentUser={currentUser!}
                  currentBoughtSymbolQuantity={currentBoughtSymbolQuantity}
                  currentBoughtSymbolId={currentBoughtSymbolId!}
          priceInputValue={priceInputValue}
          volumeInputValue={volumeInputValue}
          todayDate={todayDate}
          handleClose={handleClose}
          handleSetUserBoughtSymbolCountToVulomeInput={
            handleSetUserBoughtSymbolCountToVulomeInput
          }
          setPriceInputValue={setPriceInputValue}
          setVolumeInputValue={setVolumeInputValue}

       
        />
      </CustomTabPanel>
      <CustomTabPanel value={tabListIndexvalue} index={1}>
        <SaleTab
          currentSymbol={currentSymbol!}
        //  userProperyWatchList={userProperyWatchList!}
                  currentBoughtSymbolQuantity={currentBoughtSymbolQuantity}
          priceInputValue={priceInputValue}
          volumeInputValue={volumeInputValue}
          todayDate={todayDate}
          userCurrentBoughtSymbol={userCurrentBoughtSymbol!}
          setPriceInputValue={setPriceInputValue}
                  setVolumeInputValue={setVolumeInputValue}
                  userTradeAccount={currentUser}
          handleClose={handleClose}
          handleSetUserBoughtSymbolCountToVulomeInput={
            handleSetUserBoughtSymbolCountToVulomeInput
          }
        />
      </CustomTabPanel>
    </Box>
  );
}
