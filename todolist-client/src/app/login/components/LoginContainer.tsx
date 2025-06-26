import React from 'react';
import dynamic from 'next/dynamic';
import { Box } from '@mui/material';
import ViewKanbanIcon from '@mui/icons-material/ViewKanban';
import './styles.scss';

const LoginForm = dynamic(() => import('./LoginForm'), { ssr: true });

export const metadata = {
  title: 'Login',
  description: 'Log into the application',
};

export default function LoginContainer() {
  return (
    <Box
      className="login"
      sx={{
        borderColor: 'primary.dark'
      }}>
      <div className="login__icon">
        <ViewKanbanIcon
          sx={{
            fontSize: 70,
            color: 'primary.dark'
          }} />
      </div>
      <LoginForm />
    </Box>
  );
}