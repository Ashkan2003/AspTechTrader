import {
  Typography,
  IconButton,
  Divider,
  Skeleton,
  Menu,
  MenuItem,
} from "@mui/material";
import SettingsSuggestOutlinedIcon from "@mui/icons-material/SettingsSuggestOutlined";
import PowerSettingsNewRoundedIcon from "@mui/icons-material/PowerSettingsNewRounded";
import { useState } from "react";
import { Link } from 'react-router-dom';
import { useGetCurrentUser } from "../../features/reactQueryUser/useGetCurrentUser";


const NavAuthState = () => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null); // this is for the menu-component
    const { currentUser, isLoadingUser, error } = useGetCurrentUser()
   if (isLoadingUser)
    return (
      <div className="flex items-center ps-3">
        <Skeleton variant="rounded" sx={{display:{xs:"none",md:"block"}}} width={120} height={40} />
        <Skeleton variant="circular" width={35} height={35} />
      </div>
    );


  // this is for the menu-component
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <div className="flex items-center sm:ps-3">
        <Typography color="white" className="hidden sm:block">
                  {currentUser.userName}   
        </Typography>
          <IconButton onClick={handleClick} size="large">
            <SettingsSuggestOutlinedIcon
              fontSize="inherit"
              color="secondary"
            ></SettingsSuggestOutlinedIcon>
          </IconButton>
        <Menu
          id="basic-menu"
          anchorEl={anchorEl}
          open={open}
          onClose={handleClose}
              >
      <MenuItem onClick={handleClose}>{currentUser.emailAddress}</MenuItem>
      <MenuItem onClick={handleClose}>{currentUser.userName}</MenuItem>
        </Menu>
      </div>
      <Divider orientation="vertical" flexItem />
      <Link to="/LogOut">
        <IconButton size="large">
          <PowerSettingsNewRoundedIcon
            fontSize="inherit"
            sx={{ color: "yellow" }}
          ></PowerSettingsNewRoundedIcon>
        </IconButton>
      </Link>
    </>
  );
};

export default NavAuthState;
