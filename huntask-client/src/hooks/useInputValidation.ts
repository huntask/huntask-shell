import { InputValidator } from '@/types';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';

export function useInputValidation(
  value: string,
  onChange: (e: ChangeEvent<HTMLInputElement>) => void,
  validators?: InputValidator[],
): [boolean, string, (e: ChangeEvent<HTMLInputElement>) => void] {
  const [touched, setTouched] = useState(false);
  const [error, setError] = useState(false);
  const [helperText, setHelperText] = useState('');

  const handleChange = useCallback(
    (e: ChangeEvent<HTMLInputElement>) => {
      if (!touched) {
        setTouched(true);
      }

      onChange(e);
    },
    [touched],
  );

  useEffect(() => {
    if (!touched || !validators || validators.length === 0) {
      return;
    }

    const failed = validators.find((validate) => {
      const result = validate(value);
      return !result.isValid;
    });

    if (failed) {
      const { error } = failed(value);
      setError(true);
      setHelperText(error || 'Error');
    } else {
      setError(false);
      setHelperText('');
    }
  }, [touched, value, validators]);

  return [error, helperText, handleChange];
}
