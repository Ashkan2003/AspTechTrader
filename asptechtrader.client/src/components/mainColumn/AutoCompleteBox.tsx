import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import { useSymbols } from "../../features/reactQuerySymbols/useSymbols";
import { Skeleton, Stack } from "@mui/material";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../../GlobalRedux/store";
import { updateMainSearchBarSymbol } from "../../GlobalRedux/Features/tableSymbols/tableSymbols-slice";

export default function AutoCompleteBox() {
    //const { isLoading, dataBaseSybmols } = useSymbols();

    const isLoading = false
    const dataBaseSybmols = [{
        id: 1,
        symbolName: 1,
        volume: 1,
        lastDeal: 1,
        lastDealPercentage: 1,
        lastPrice: 1,
        lastPricePercentage: 1,
        theFirst: 1,
        theLeast: 1,
        theMost: 1,
        demandVolume: 1,
        demandPrice: 1,
        offerPrice: 1,
        offerVolume: 1,
        state: 1,
        chartNumber: 1,
    }]

  const dispatch = useDispatch<AppDispatch>();

  if (isLoading)
    return (
      <Stack paddingX={2}>
        <Skeleton sx={{width:{sx:3,md:300}}}  height="50px" />
      </Stack>
    );

  // we want the entire symbols-name to give them to the options-prperty of auto-compelte-box to show them to the user
  // the optionSymbols is an array of symbols-name
  const optionSymbols = dataBaseSybmols?.map((symbol) => {
    return symbol.symbolName;
  });

  return (
    <div>
      <Autocomplete
        onChange={(event, value: any) => {
          dispatch(updateMainSearchBarSymbol(value));
        }}
        className=" bg-[#ffff] dark:bg-[#39566b]"
        disablePortal
        id="combo-box-demo"
        size="small"
        options={optionSymbols!}
        sx={{
          color: "secondary",
          borderRadius: "5px",
          width: { xs:"280",md: "230px", lg: "400px" },
          display: {  sm: "none",md:"block" },
        }}
        renderInput={(params) => (
          <TextField color="info" {...params} label="جستجوی نماد" />
        )}
      />
    </div>
  );
}

