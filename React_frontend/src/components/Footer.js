import React from 'react';
import { Box, Typography } from '@mui/material';

const Footer = () => {
  return (
    <Box
      component="footer"
      sx={{
        mt: 'auto',
        py: 2,
        px: 2,
        backgroundColor: (theme) => theme.palette.grey[100],
        borderTop: (theme) => `1px solid ${theme.palette.divider}`,
        textAlign: 'center',
      }}
    >
      <Typography variant="body2" color="textSecondary">
        Utvecklat av <strong>Beruk Gebru</strong> & <strong>Raman Rostam</strong>
      </Typography>
    </Box>
  );
};

export default Footer;
