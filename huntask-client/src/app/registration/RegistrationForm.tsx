/* eslint-disable @typescript-eslint/no-unused-vars */
'use client';

import React, { useActionState, useCallback, useReducer, useState } from 'react';
import {
  AddInformationStep,
  CreateAccountStep,
  RegistrationActions,
  RegistrationStepper,
  RegistrationSuccessStep,
} from './components';
import { RegistrationFormProvider } from './context/RegistrationFormContext';
import { initialRegistrationFormState, RegistrationFormState } from './types';
import { registrationAction } from './actions';
import './styles.scss';

const steps = ['Create account', 'Add information', 'Done'];

function isLastStep(steps: string[], step: number) {
  return step >= steps.length - 1;
}

function isFirstStep(step: number) {
  return step <= 0;
}

const reducer = (
  state: RegistrationFormState,
  action: Partial<RegistrationFormState>,
): RegistrationFormState => {
  return { ...state, ...action };
};

export default function RegistrationForm() {
  const [activeStep, setActiveStep] = useState(0);
  const [formData, setFormData] = useReducer(reducer, initialRegistrationFormState);
  const [state, formAction, isPending] = useActionState(
    registrationAction,
    initialRegistrationFormState,
  );

  const nextAction = useCallback(() => {
    setActiveStep((prev) => (isLastStep(steps, prev) ? prev : prev + 1));
  }, [setActiveStep]);

  const backAction = useCallback(() => {
    setActiveStep((prev) => (isFirstStep(prev) ? prev : prev - 1));
  }, [setActiveStep]);

  const onAvatarUpload = useCallback((file: File) => {
    setFormData({ avatar: file.name });
    console.info(file);
  }, []);

  return (
    <RegistrationFormProvider>
      <form className="registration-form__form" action={formAction}>
        <RegistrationStepper steps={steps} activeStep={activeStep} />
        {activeStep === 0 && <CreateAccountStep />}
        {activeStep === 1 && <AddInformationStep onAvatarUpload={onAvatarUpload} />}
        {activeStep === 2 && <RegistrationSuccessStep />}
        <RegistrationActions
          nextLabel="Next"
          backLabel="Back"
          isPending={isPending}
          isNextDisabled={isLastStep(steps, activeStep)}
          isBackDisabled={isFirstStep(activeStep)}
          nextAction={nextAction}
          backAction={backAction}
        />
      </form>
    </RegistrationFormProvider>
  );
}
