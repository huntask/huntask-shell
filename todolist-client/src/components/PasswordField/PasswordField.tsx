'use client';

import React, { useState } from 'react';
import { FormControl, FormHelperText, InputLabel, OutlinedInput } from '@mui/material';
import { useInputValidation } from '@/hooks/useInputValidation';
import { ShowPasswordAdornment } from './adornments/ShowPasswordAdornment';
import { InputValidator } from '@/types';

type PasswordFieldProps = {
  name?: string;
  label?: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  validators?: InputValidator[];
};

export function PasswordField({
  name = 'password',
  label = 'Password',
  value = '',
  onChange,
  validators,
}: PasswordFieldProps) {
  const [showPassword, setShowPassword] = useState(false);
  const [error, helperText, handleChange] = useInputValidation(value, onChange, validators);

  const handleShowPassword = () => setShowPassword((show) => !show);

  return (
    <FormControl fullWidth variant="outlined" margin="normal" error={error}>
      <InputLabel htmlFor={name}>{label}</InputLabel>
      <OutlinedInput
        fullWidth
        name={name}
        type={showPassword ? 'text' : 'password'}
        endAdornment={
          <ShowPasswordAdornment
            showPassword={showPassword}
            handleShowPassword={handleShowPassword}
          />
        }
        label={label}
        value={value}
        onChange={handleChange}
      />
      {error && <FormHelperText>{helperText}</FormHelperText>}
    </FormControl>
  );
}
