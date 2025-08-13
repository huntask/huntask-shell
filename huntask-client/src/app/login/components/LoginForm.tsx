'use client';

import React, { useActionState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { Button } from '@mui/material';
import LoginIcon from '@mui/icons-material/Login';
import { TextInputField, PasswordField } from '@/components';
import { defaultLoginActionState, loginAction } from '../actions/loginAction';
import './styles.scss';

export default function LoginForm() {
  const [email, setEmail] = React.useState('');
  const [password, setPassword] = React.useState('');
  const [state, formAction, isPending] = useActionState(loginAction, defaultLoginActionState);
  const router = useRouter();

  useEffect(() => {
    if (state.success) {
      router.push('/');
    }
  }, [router, state]);

  return (
    <form className="login-form__form" action={formAction}>
      <TextInputField
        name="email"
        label="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
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
