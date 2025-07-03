/* eslint-disable @typescript-eslint/no-unused-vars */
'use client';

import React, { useCallback, useState } from 'react';
import { Avatar, Button } from '@mui/material';
import CloudUpload from '@mui/icons-material/CloudUpload';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { VisuallyHiddenInput } from './VisuallyHiddenInput';
import './styles.scss';

const avatarStyle = {
  width: 128,
  height: 128,
};

const iconStyle = {
  fontSize: 128,
  color: 'info.light',
};

export type UploadAvatarProps = {
  avatar?: string | null;
  onUpload: (file: File) => void;
};

export function UploadAvatar({ avatar, onUpload }: UploadAvatarProps) {
  const fileChangeHandler = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      const file = event.target.files?.[0];
      if (file) {
        onUpload(file);
        return;
      }

      console.error('File upload error: No file selected');
    },
    [onUpload],
  );

  return (
    <div className="upload-avatar">
      {avatar && <Avatar src={avatar} sx={avatarStyle}></Avatar>}
      {!avatar && <AccountCircleIcon sx={iconStyle} />}
      <Button
        className="upload-avatar__button"
        component="label"
        variant="contained"
        tabIndex={-1}
        role={undefined}
        startIcon={<CloudUpload />}
      >
        Upload Photo
        <VisuallyHiddenInput type="file" onChange={fileChangeHandler} />
      </Button>
    </div>
  );
}
