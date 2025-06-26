/* eslint-disable @typescript-eslint/no-unused-vars */
'use client';

import React, { useActionState } from 'react';
import { Button, TextField } from '@mui/material';
import LoginIcon from '@mui/icons-material/Login';
import PasswordField from '@/components/PasswordField/PasswordField';
import { loginAction } from '../actions';
import './styles.scss';

export default function LoginForm() {
  const [state, formAction, isPending] = useActionState(loginAction, { error: null });

  return (
    <form className="login-form__form" action={formAction}>
      <TextField
        required
        fullWidth
        label="Login"
        name="username"
        variant="outlined"
        margin="normal" />
      <PasswordField />

      <div className="login-form__actions">
        <Button
          fullWidth
          type="submit"
          variant="contained"
          size="large"
          loadingPosition="start"
          loading={isPending}
          startIcon={<LoginIcon />}>
          Login
        </Button>
        <Button
          fullWidth
          variant="outlined"
          size="large">
          Register
        </Button>
      </div>
    </form>
  );
}
