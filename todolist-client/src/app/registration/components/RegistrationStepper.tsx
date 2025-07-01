'use client';

import React from 'react';
import { Step, StepLabel, Stepper } from '@mui/material';
import '../styles.scss';

type StepperProps = {
  steps: string[];
  activeStep: number;
};

export function RegistrationStepper({ steps, activeStep }: StepperProps) {
  return (
    <div className="registration-form__stepper">
      <Stepper activeStep={activeStep} alternativeLabel>
        {steps.map((label) => (
          <Step key={label}>
            <StepLabel>{label}</StepLabel>
          </Step>
        ))}
      </Stepper>
    </div>
  );
}
