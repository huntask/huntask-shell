'use client';

import React, { useEffect } from 'react';
import { TextInputField, PasswordField } from '@/components';
import { validateEmail } from '@/components/TextInputField/validators';
import { validatePassword } from '@/components/PasswordField/validators';
import { useFormField } from '../../hooks/useFormField';
import '../../styles.scss';

type CreateAccountStepProps = {
  setValidity?: (isValid: boolean) => void;
};

export function CreateAccountStep({ setValidity }: CreateAccountStepProps) {
  const [email, setEmail] = useFormField('email');
  const [password, setPassword] = useFormField('password');
  const [confirmPassword, setConfirmPassword] = useFormField('confirmPassword');

  useEffect(() => {
    const emailValidity: boolean = validateEmail(email ?? '')?.isValid;
    const passwordValidity: boolean = validatePassword(password ?? '')?.isValid;
    const confirmPasswordValidity: boolean = validatePassword(confirmPassword ?? '')?.isValid;
    const isPasswordsMatch: boolean = password === confirmPassword;

    setValidity?.(emailValidity && passwordValidity && confirmPasswordValidity && isPasswordsMatch);
  }, [email, password, confirmPassword, setValidity]);

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
