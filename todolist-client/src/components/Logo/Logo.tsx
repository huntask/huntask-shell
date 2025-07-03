'use client';

import React from 'react';
import ViewKanbanIcon from '@mui/icons-material/ViewKanban';

export type LogoProps = {
  className?: string;
};

const iconStyle = {
  fontSize: 70,
  color: 'primary.dark',
};

export function Logo({ className }: LogoProps) {
  return (
    <div className={`${className}__icon`}>
      <ViewKanbanIcon sx={iconStyle} />
    </div>
  );
}
