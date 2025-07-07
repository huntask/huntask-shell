'use client';

import React, { useContext } from 'react';
import { TextInputField, UploadAvatar } from '@/components';
import { useFormField } from '../../hooks/useFormField';
import { RegistrationFormContext } from '../../context/RegistrationFormContext';
import '../../styles.scss';

type AddInformationStepProps = {
  onAvatarUpload: (file: File) => void;
};

export function AddInformationStep({ onAvatarUpload }: AddInformationStepProps) {
  const { formData } = useContext(RegistrationFormContext);
  const [firstName, setFirstName] = useFormField('firstName');
  const [lastName, setLastName] = useFormField('lastName');

  return (
    <div className="add-information-step">
      <div className="add-information-step__fields">
        <UploadAvatar avatar={formData.avatar} onUpload={onAvatarUpload} />
        <TextInputField
          name="firstName"
          label="FirstName"
          value={firstName}
          onChange={setFirstName}
        />
        <TextInputField name="lastName" label="LastName" value={lastName} onChange={setLastName} />
      </div>
    </div>
  );
}
