'use client';

import React, { useActionState, useCallback, useContext, useState } from 'react';
import {
  AddInformationStep,
  CreateAccountStep,
  RegistrationActions,
  RegistrationStepper,
  RegistrationSuccessStep,
} from './components';
import { RegistrationFormContext } from './context/RegistrationFormContext';
import { initialRegistrationFormState, RegistrationFormState } from './models/registrationFormState';
import './styles.scss';
import { register } from './services';

const steps = ['Create account', 'Add information', 'Done'];

function isLastStep(steps: string[], step: number) {
  return step >= steps.length - 1;
}

function isFirstStep(step: number) {
  return step <= 0;
}

export default function RegistrationForm() {
  const { formData, setFormData } = useContext(RegistrationFormContext);
  const [activeStep, setActiveStep] = useState(0);
  const [isCreateAccountStepValid, setCreateAccountStepValidity] = useState(false);
  const [isAddInformationStepValid, setAddInformationStepValidity] = useState(false);
  const [state, setState] = useState(initialRegistrationFormState);

  const nextAction = useCallback(() => {
    setActiveStep((prev) => (isLastStep(steps, prev) ? prev : prev + 1));
  }, [setActiveStep]);

  const backAction = useCallback(() => {
    setActiveStep((prev) => (isFirstStep(prev) ? prev : prev - 1));
  }, [setActiveStep]);

  const isAddInformationStepDisabled = useCallback((): boolean => {
    return activeStep == 0 && (!isCreateAccountStepValid || isLastStep(steps, activeStep));
  }, [activeStep, isCreateAccountStepValid]);

  const isRegistrationSuccessStepDisabled = useCallback((): boolean => {
    return activeStep == 1 && (!isAddInformationStepValid || isLastStep(steps, activeStep));
  }, [activeStep, isAddInformationStepValid]);

  const isNextDisabled = (): boolean => {
    return isAddInformationStepDisabled() || isRegistrationSuccessStepDisabled() || isLastStep(steps, activeStep);
  }

  const submitAction = useCallback(async (): Promise<void> => {
    // TODO[registration]: utilise useQuery
    const result: RegistrationFormState = await register(initialRegistrationFormState, formData);
    setState(result)

    if (result.success) {
      nextAction();
    }
  }, [formData, nextAction]);

  return (
    <form className="registration-form__form">
      <RegistrationStepper steps={steps} activeStep={activeStep} />
      {activeStep === 0 && <CreateAccountStep setValidity={setCreateAccountStepValidity} />}
      {activeStep === 1 && <AddInformationStep setValidity={setAddInformationStepValidity} />}
      {activeStep === 2 && <RegistrationSuccessStep />}
      <RegistrationActions
        nextLabel="Next"
        backLabel="Back"
        // isPending={isPending}
        isNextDisabled={isNextDisabled()}
        isBackDisabled={isFirstStep(activeStep)}
        isSubmitStep={activeStep == 1}
        nextAction={nextAction}
        backAction={backAction}
        submitAction={submitAction}
      />
      <ul className="registration-form__errors">
        {state?.errors?.length && (
          state.errors.map((error, index) => (
            <li key={index} className="registration-form__error">
              {error}
            </li>
        )))}
      </ul>
    </form>
  );
}
