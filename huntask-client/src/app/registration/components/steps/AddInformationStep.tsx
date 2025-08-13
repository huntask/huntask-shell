'use client';

import React, { useEffect } from 'react';
import { TextInputField, UploadAvatar } from '@/components';
import { useFormField } from '../../hooks/useFormField';
import '../../styles.scss';

type AddInformationStepProps = {
  setValidity?: (isValid: boolean) => void;
};

function isNonEmptyString(value: string | undefined): boolean {
  return typeof value === 'string' && value.trim().length > 0;
}

export function AddInformationStep({ setValidity }: AddInformationStepProps) {
  const [firstName, setFirstName] = useFormField('firstName');
  const [lastName, setLastName] = useFormField('lastName');
  const [avatar, setAvatar] = useFormField('avatar');

  useEffect(() => {
    const firstNameValidity: boolean = isNonEmptyString(firstName);
    const lastNameValidity: boolean = isNonEmptyString(lastName);

    setValidity?.(firstNameValidity && lastNameValidity);
  }, [firstName, lastName, setValidity]);

  return (
    <div className="add-information-step">
      <div className="add-information-step__fields">
        <UploadAvatar avatar={avatar} onUpload={setAvatar} />
        <TextInputField
          name="firstName"
          label="FirstName"
          value={firstName}
          onChange={setFirstName}
          required
        />
        <TextInputField name="lastName" label="LastName" value={lastName} onChange={setLastName} required />
      </div>
    </div>
  );
}
