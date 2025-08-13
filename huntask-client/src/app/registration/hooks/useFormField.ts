import { ChangeEvent, useContext } from 'react';
import { RegistrationFormContext } from '../context/RegistrationFormContext';
import { RegistrationFormData } from '../models';

export function useFormField<K extends keyof RegistrationFormData>(name: K): [RegistrationFormData[K], (e: ChangeEvent<HTMLInputElement>) => void] {
  const { formData, setFormData } = useContext(RegistrationFormContext);

  const value = formData[name];

  const onChange = (e: ChangeEvent<HTMLInputElement>) => {
    const newValue = e.target.type === 'file'
        ? e.target.files?.[0] ?? null
        : e.target.value;

    setFormData((prev) => ({
      ...prev,
      [name]: newValue as RegistrationFormData[K],
    }));
  };

  return [value, onChange];
}
