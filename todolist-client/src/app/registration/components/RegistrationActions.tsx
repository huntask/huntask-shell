'use client';

import React from 'react';
import { Button } from '@mui/material';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import '../styles.scss';

type RegistrationActionsProps = {
  isPending?: boolean;
  isNextDisabled: boolean;
  isBackDisabled: boolean;
  nextLabel: string;
  backLabel: string;
  nextAction: () => void;
  backAction: () => void;
};

export function RegistrationActions({
  isPending = false,
  isNextDisabled,
  isBackDisabled,
  nextLabel,
  backLabel,
  nextAction,
  backAction,
}: RegistrationActionsProps) {
  return (
    <div className="registration-actions">
      <Button
        variant="outlined"
        size="large"
        loadingPosition="start"
        loading={isPending}
        startIcon={<NavigateBeforeIcon />}
        onClick={backAction}
        disabled={isBackDisabled}
      >
        {backLabel}
      </Button>
      <Button
        type="submit"
        variant="contained"
        size="large"
        loadingPosition="start"
        loading={isPending}
        endIcon={<NavigateNextIcon />}
        onClick={nextAction}
        disabled={isNextDisabled}
      >
        {nextLabel}
      </Button>
    </div>
  );
}
