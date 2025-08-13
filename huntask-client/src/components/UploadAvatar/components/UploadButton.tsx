'use client';

import React, { useCallback } from 'react';
import { Button } from '@mui/material';
import CloudUpload from '@mui/icons-material/CloudUpload';
import { VisuallyHiddenInput } from '../VisuallyHiddenInput';
import '../styles.scss';

export type UploadButtonProps = {
  createPreview: (file?: File | null) => null | undefined;
  onUpload: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

export function UploadButton({ createPreview, onUpload }: UploadButtonProps) {
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
  );
}
