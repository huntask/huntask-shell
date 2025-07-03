'use client';

import React from 'react';
import { Button } from '@mui/material';
import LoginIcon from '@mui/icons-material/Login';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import '../../styles.scss';

export function RegistrationSuccessStep() {
  return (
    <div className="registration-success">
      <CheckCircleIcon
        sx={{
          fontSize: 70,
          color: 'success.light',
        }}
      />
      <h2>Congratulations, your account is created!</h2>
      <a className="registration-success__link" href="/login">
        <Button
          variant="contained"
          size="large"
          sx={{
            bgcolor: 'success.light',
          }}
          endIcon={<LoginIcon />}
        >
          Login
        </Button>
      </a>
    </div>
  );
}
