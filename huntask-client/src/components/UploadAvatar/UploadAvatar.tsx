'use client';

import React, { useCallback, useEffect, useState } from 'react';
import { Avatar } from '@mui/material';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { UploadButton } from './components/UploadButton';
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
  avatar?: File | null;
  onUpload: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

export function UploadAvatar({ avatar, onUpload }: UploadAvatarProps) {
  const [preview, setPreview] = useState<string | null>(null);
  const createPreview = useCallback((file?: File | null) => {
    if (!file) {
      return null;
    }

    const url = URL.createObjectURL(file);
    setPreview((oldUrl: string | null) => {
      if (oldUrl) {
        URL.revokeObjectURL(oldUrl);
      }

      return url;
    });
  }, [setPreview]);

  useEffect(() => {
    createPreview(avatar);
  }, [createPreview, avatar]);

  const fileChangeHandler = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      onUpload(event);

      const file = event.target.files?.[0];
      if (file) {
        createPreview(file);
        return;
      }

      console.error('File upload error: No file selected');
    }, [createPreview, onUpload]);

  return (
    <div className="upload-avatar">
      {preview && <Avatar src={preview} sx={avatarStyle}></Avatar>}
      {!preview && <AccountCircleIcon sx={iconStyle} />}
      <UploadButton createPreview={createPreview} onUpload={onUpload} />
    </div>
  );
}
