import { Skeleton, Typography } from "@mui/material";
import { useGetCurrentUser } from "../../features/reactQueryUser/useGetCurrentUser";

const UserProperty = () => {

    const { currentUser, isLoadingUser, error } = useGetCurrentUser()

  return (
    <div className=" px-4 hidden md:flex items-center">
      <Typography className="text-white">مانده قابل معامله:</Typography>
      {isLoadingUser ? (
        <Skeleton sx={{marginLeft:"10px"}} width="80px" height="50px" />
      ) : (
        <Typography className="text-yellow-300 ps-1">
                      {currentUser?.userProperty} ریال
        </Typography>
      )}
    </div>
  );
};

export default UserProperty;
