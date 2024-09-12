import { Typography } from '@mui/material';
import { Link } from 'react-router-dom';

export default function Copyright(props: any) {
    return (
      <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
        <Link className="text-blue-600" to="">
          Your Website
        </Link>{' '}
        {new Date().getFullYear()}
        {'.'}
      </Typography>
    );
  }