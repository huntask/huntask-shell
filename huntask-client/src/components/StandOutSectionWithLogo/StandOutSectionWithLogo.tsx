import React from 'react';
import { Logo } from '../Logo/Logo';
import { StandOutSection, StandOutSectionProps } from '../StandOutSection/StandOutSection';

export type StandOutSectionWithLogoProps = {} & StandOutSectionProps;

export function StandOutSectionWithLogo({ className, children }: StandOutSectionWithLogoProps) {
  return (
    <StandOutSection className={className}>
      <Logo className={className} />
      {children}
    </StandOutSection>
  );
}
