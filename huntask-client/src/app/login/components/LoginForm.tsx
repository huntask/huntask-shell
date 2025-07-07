/* eslint-disable @typescript-eslint/no-unused-vars */
'use client';

import React, { useActionState } from 'react';
import { Button } from '@mui/material';
import LoginIcon from '@mui/icons-material/Login';
import { TextInputField, PasswordField } from '@/components';
import { loginAction } from '../actions';
import './styles.scss';

export default function LoginForm() {
  const [username, setUsername] = React.useState('');
  const [password, setPassword] = React.useState('');
  const [state, formAction, isPending] = useActionState(loginAction, { error: null });

  return (
    <form className="login-form__form" action={formAction}>
      <TextInputField
        name="email"
        label="Email"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <PasswordField value={password} onChange={(e) => setPassword(e.target.value)} />

      <div className="login-form__actions">
        <Button
          fullWidth
          type="submit"
          variant="contained"
          size="large"
          loadingPosition="start"
          loading={isPending}
          startIcon={<LoginIcon />}
        >
          Login
        </Button>
        <a href="/registration">
          <Button fullWidth variant="outlined" size="large">
            Register
          </Button>
        </a>
      </div>
    </form>
  );
}
