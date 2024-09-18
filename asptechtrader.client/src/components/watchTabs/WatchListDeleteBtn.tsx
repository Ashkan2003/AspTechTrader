import { IconButton } from "@mui/material";
import HighlightOffRoundedIcon from "@mui/icons-material/HighlightOffRounded";
import { useDeleteWatchList } from "../../features/reactQueryWatchList/useDeleteWatchList";

interface Props {
    userId: string;
    userWatchListId:string
}
// this is the watch-list delete-btn
const WatchListDeleteBtn = ({ userId,userWatchListId }: Props) => {
  const { mutate } = useDeleteWatchList();
  // this function is for deleting the selected watch from the watchList by its id
    const handleListDeleteBtn = (userId: string) => {
        mutate({ userId: userId, userWatchListId: userWatchListId });
  };
  return (
    <IconButton onClick={() => handleListDeleteBtn(userId)} size="medium">
      <HighlightOffRoundedIcon fontSize="inherit" color="warning" />
    </IconButton>
  );
};

export default WatchListDeleteBtn;
