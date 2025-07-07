'use client';

import React from 'react';
import { TextInputField, PasswordField } from '@/components';
import { validateEmail } from '@/components/TextInputField/validators';
import { validatePassword } from '@/components/PasswordField/validators';
import { useFormField } from '../../hooks/useFormField';
import '../../styles.scss';


export function CreateAccountStep() {
  const [email, setEmail] = useFormField('email');
  const [password, setPassword] = useFormField('password');
  const [confirmPassword, setConfirmPassword] = useFormField('confirmPassword');

  const passwordValidators = [
    validatePassword,
    () => {
      return password === confirmPassword
        ? { isValid: true }
        : { isValid: false, error: 'Passwords do not match.' };
    },
  ];

  return (
    <div className="create-account-step">
      <div className="create-account-step__fields">
        <TextInputField
          name="email"
          label="Email"
          value={email}
          onChange={setEmail}
          validators={[validateEmail]}
        />
        <PasswordField
          name="password"
          label="New Password"
          value={password}
          onChange={setPassword}
          validators={passwordValidators}
        />
        <PasswordField
          name="confirmPassword"
          label="Confirm Password"
          value={confirmPassword}
          onChange={setConfirmPassword}
          validators={passwordValidators}
        />
      </div>
    </div>
  );
}
