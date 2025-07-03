'use client';

import React from 'react';
import { TextField } from '@mui/material';
import { useInputValidation } from '@/hooks/useInputValidation';
import { InputValidator } from '@/types';

type TextFieldProps = {
  name: string;
  label: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  validators?: InputValidator[];
};

export function TextInputField({ name, label, value = '', onChange, validators }: TextFieldProps) {
  const [error, helperText, handleChange] = useInputValidation(value, onChange, validators);

  return (
    <TextField
      fullWidth
      label={label}
      name={name}
      variant="outlined"
      margin="normal"
      value={value}
      onChange={handleChange}
      {...(error ? { error, helperText } : {})}
    />
  );
}
