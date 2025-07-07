import { ChangeEvent, useContext } from 'react';
import { RegistrationFormContext } from '../context/RegistrationFormContext';
import { RegistrationFormState } from '../types';

type FormField = [string, (e: ChangeEvent<HTMLInputElement>) => void];

export function useFormField<K extends keyof RegistrationFormState>(name: K): FormField {
  const { formData, setFormData } = useContext(RegistrationFormContext);

  const value = formData[name] || '';

  const onChange = (e: ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      [name]: e.target.value,
    }));
  };

  return [value, onChange];
}
