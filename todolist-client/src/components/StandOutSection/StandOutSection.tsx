import React, { ReactNode } from 'react';
import { Box } from '@mui/material';

export type StandOutSectionProps = {
  className?: string;
  children?: ReactNode;
};

export function StandOutSection({ className, children }: StandOutSectionProps) {
  return (
    <Box className={className} sx={{ borderColor: 'primary.dark' }}>
      {children}
    </Box>
  );
}
