import AccountTreeIcon from "@mui/icons-material/AccountTree";
import { Skeleton, Stack } from "@mui/material";
import Box from "@mui/material/Box";
import List from "@mui/material/List";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import React, { useState } from "react";
import { useUserWatchLists } from "../../features/reactQueryWatchList/useUserWatchLists";
import WatchListDeleteBtn from "./WatchListDeleteBtn";
import WatchListFormDialog from "./WatchListFormDialog";
import { AppDispatch } from "../../GlobalRedux/store";
import { useDispatch } from "react-redux";
import { updateCurrentShowedWatchListSymbols } from "../../GlobalRedux/Features/tableSymbols/tableSymbols-slice";
import UserWatchInput from "./UserWatchInput";
import { SymbolType } from "../../types/types";


interface Props {
    currentUser: any;
}

// this component is for adding, deleting,and updating a watch
export default function UserWatchList({currentUser }:Props) {
  const [inputValue, setInputValue] = useState(""); // the value of input
    const [selectedIndex, setSelectedIndex] = useState(1); // the current selected watch from the list



  const { isLoading, userWatchList } = useUserWatchLists(currentUser.userId);
  const dispatch = useDispatch<AppDispatch>();

   //if is loading return a skeleton
  if (isLoading)
    return (
      <Stack paddingTop={2} paddingX={1} spacing={1}>
        <Stack gap={1} flexDirection="row">
          <Skeleton
            animation="wave"
            variant="rounded"
            width={300}
            height={50}
          />
          <Skeleton animation="wave" variant="rounded" width={70} height={50} />
        </Stack>
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
        <Skeleton animation="wave" variant="rounded" width="full" height={35} />
      </Stack>
    );



  // this function is for activating the selected watch by adding some style
  const handleListItemClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>,
      index: number,
      selectedWatchSymbols: SymbolType[]
  ) => {
    setSelectedIndex(index);
      // every time the user selects a watch then send this watch-symbols to the redux
    dispatch(updateCurrentShowedWatchListSymbols(selectedWatchSymbols));
  };

  return (
    <div className="flex items-center flex-col ">
      <Box
        sx={{
          width: "100%",
          maxWidth: 360,
          color: "textPallet.main",
          bgcolor: "ternery.dark",
        }}
      >
        {/* the input fied */}
        <UserWatchInput inputValue={inputValue} setInputValue={setInputValue} currentUserId={currentUser.userId} />
        {/* the list */}
        <List
          component="nav"
          sx={{
            width: "100%",
            maxWidth: 360,
            bgcolor: "ternery.main",
              position: "relative",
              maxHeight: 150,
              overflowY: "auto",
            // "& ul": { padding: 0 },
          }}
              >
           {userWatchList?.map((item, index) => (
            <div
              className={`flex ${
                selectedIndex === index && "bg-[#e6e8ea] dark:bg-[#212121]"
                       }`}
                   key={item.userWatchListId}
            >
              <ListItemButton
                // selected={selectedIndex === index}
                       onClick={(event) =>
                       handleListItemClick(event, index, item.symbols)
                }
              >
                <ListItemIcon>
                  <AccountTreeIcon color="info" />
                </ListItemIcon>
                <ListItemText primary={item.userWatchListName} />
                   </ListItemButton>
               {item.userWatchListName === "دارایی های من" || ( // with this code i said that dont render(map) the  "دارایی های من"-watchList so the user canot delete or edit the first-watch-List
                <div className="flex">
               {/* this is the dialog-btn */}
               <WatchListFormDialog userWatchList={item} />
               {/*  this is the watch-list delete-btn */}
               <WatchListDeleteBtn userId={currentUser.userId} userWatchListId={item.userWatchListId} />
                </div>
              )}
            </div>
          ))}
        </List>
      </Box>
    </div>
  );
}
