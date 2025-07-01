'use client';

import React from 'react';
import { IconButton, InputAdornment } from '@mui/material';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

type ShowPasswordAdornmentProps = {
  showPassword: boolean;
  handleShowPassword: () => void;
};

export function ShowPasswordAdornment({
  showPassword,
  handleShowPassword,
}: ShowPasswordAdornmentProps) {
  return (
    <InputAdornment position="end">
      <IconButton onClick={handleShowPassword} edge="end">
        {showPassword ? <VisibilityOff /> : <Visibility />}
      </IconButton>
    </InputAdornment>
  );
}
