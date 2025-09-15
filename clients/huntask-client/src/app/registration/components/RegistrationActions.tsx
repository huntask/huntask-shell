'use client';

import React, { MouseEvent, MouseEventHandler, useCallback } from 'react';
import { Button } from '@mui/material';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import '../styles.scss';

type RegistrationActionsProps = {
  isPending?: boolean;
  isNextDisabled: boolean;
  isBackDisabled: boolean;
  isSubmitStep: boolean;
  nextLabel: string;
  backLabel: string;
  nextAction: () => void;
  backAction: () => void;
  submitAction: () => Promise<void>;
};

export function RegistrationActions({
  isPending = false,
  isNextDisabled,
  isBackDisabled,
  isSubmitStep,
  nextLabel,
  backLabel,
  nextAction,
  backAction,
  submitAction
}: RegistrationActionsProps) {
  const onNext: MouseEventHandler<HTMLButtonElement> = useCallback((event: MouseEvent<HTMLButtonElement>): void => {
    event.preventDefault();

    if (isSubmitStep) {
      submitAction();
    }
  }, [isSubmitStep, submitAction]);

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
        variant="contained"
        size="large"
        loadingPosition="start"
        loading={isPending}
        endIcon={<NavigateNextIcon />}
        disabled={isNextDisabled}
        onClick={onNext}
      >
        {nextLabel}
      </Button>
    </div>
  );
}
