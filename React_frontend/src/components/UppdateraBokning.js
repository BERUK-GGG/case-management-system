import React, { useState, useEffect } from 'react';
import {
  Container,
  Paper,
  Typography,
  TextField,
  Button,
  Box,
  Alert,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  CircularProgress
} from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { useForm, Controller } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import 'dayjs/locale/sv';
import { visitService, customerService } from '../services/api';

dayjs.locale('sv');

const UppdateraBokning = () => {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);
  const [visits, setVisits] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [selectedVisit, setSelectedVisit] = useState(null);
  const navigate = useNavigate();

  const { register, handleSubmit, control, setValue, formState: { errors } } = useForm();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [visitsResponse, customersResponse] = await Promise.all([
          visitService.getAll(),
          customerService.getAll()
        ]);
        setVisits(visitsResponse.data);
        setCustomers(customersResponse.data);
      } catch (err) {
        setError('Kunde inte hämta data');
      }
    };
    fetchData();
  }, []);

  const handleVisitSelect = (visitId) => {
    const visit = visits.find(v => v.id === parseInt(visitId));
    if (visit) {
      setSelectedVisit(visit);
      // Populate form with existing visit data
      setValue('customerId', visit.customerId);
      setValue('description', visit.description);
      setValue('notes', visit.notes || '');
      setValue('status', visit.status || 'Schemalagd');
    }
  };

  const onSubmit = async (data) => {
    if (!selectedVisit) {
      setError('Välj en bokning att uppdatera');
      return;
    }

    setLoading(true);
    setError('');
    setSuccess('');

    try {
      const visitData = {
        customerId: parseInt(data.customerId),
        visitDate: data.visitDate.toISOString(),
        description: data.description,
        notes: data.notes || null,
        status: data.status
      };

      await visitService.update(selectedVisit.id, visitData);
      setSuccess('Bokning uppdaterad framgångsrikt!');
      
      // Refresh visits list
      const response = await visitService.getAll();
      setVisits(response.data);
      
      setTimeout(() => {
        navigate('/');
      }, 2000);
    } catch (err) {
      setError(err.response?.data?.message || 'Ett fel uppstod vid uppdatering av bokning');
    } finally {
      setLoading(false);
    }
  };

  const getCustomerName = (customerId) => {
    const customer = customers.find(c => c.id === customerId);
    return customer ? `${customer.firstName} ${customer.lastName}` : 'Okänd kund';
  };

  const formatVisitForDisplay = (visit) => {
    const customer = customers.find(c => c.id === visit.customerId);
    const customerName = customer ? `${customer.firstName} ${customer.lastName}` : 'Okänd kund';
    const visitDate = new Date(visit.visitDate).toLocaleString('sv-SE');
    return `${visitDate} - ${customerName} - ${visit.description}`;
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="sv">
      <Container maxWidth="md" sx={{ mt: 4 }}>
        <Paper elevation={3} sx={{ p: 4 }}>
          <Typography variant="h4" component="h1" gutterBottom align="center">
            Uppdatera Bokning
          </Typography>

          {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
          {success && <Alert severity="success" sx={{ mb: 2 }}>{success}</Alert>}

          <Box sx={{ mt: 2 }}>
            <FormControl fullWidth margin="normal">
              <InputLabel>Välj bokning att uppdatera</InputLabel>
              <Select
                label="Välj bokning att uppdatera"
                onChange={(e) => handleVisitSelect(e.target.value)}
                defaultValue=""
              >
                {visits.map((visit) => (
                  <MenuItem key={visit.id} value={visit.id}>
                    {formatVisitForDisplay(visit)}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            {selectedVisit && (
              <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 3 }}>
                <Typography variant="h6" gutterBottom sx={{ mt: 3 }}>
                  Uppdatera bokning för: {getCustomerName(selectedVisit.customerId)}
                </Typography>

                <FormControl fullWidth margin="normal" error={!!errors.customerId}>
                  <InputLabel>Kund</InputLabel>
                  <Select
                    {...register('customerId', { required: 'Kund måste väljas' })}
                    label="Kund"
                    defaultValue={selectedVisit.customerId}
                  >
                    {customers.map((customer) => (
                      <MenuItem key={customer.id} value={customer.id}>
                        {customer.firstName} {customer.lastName} - {customer.phoneNumber}
                      </MenuItem>
                    ))}
                  </Select>
                  {errors.customerId && (
                    <Typography variant="caption" color="error">
                      {errors.customerId.message}
                    </Typography>
                  )}
                </FormControl>

                <Controller
                  name="visitDate"
                  control={control}
                  defaultValue={dayjs(selectedVisit.visitDate)}
                  rules={{ required: 'Datum och tid är obligatoriskt' }}
                  render={({ field, fieldState: { error } }) => (
                    <DateTimePicker
                      label="Datum och tid"
                      value={field.value}
                      onChange={(date) => field.onChange(date)}
                      slotProps={{
                        textField: {
                          fullWidth: true,
                          margin: 'normal',
                          error: !!error,
                          helperText: error?.message
                        }
                      }}
                      minDateTime={dayjs()}
                    />
                  )}
                />

                <TextField
                  fullWidth
                  label="Beskrivning av besök"
                  margin="normal"
                  multiline
                  rows={3}
                  {...register('description', { 
                    required: 'Beskrivning är obligatorisk',
                    minLength: { value: 5, message: 'Beskrivning måste vara minst 5 tecken' }
                  })}
                  error={!!errors.description}
                  helperText={errors.description?.message}
                />

                <FormControl fullWidth margin="normal">
                  <InputLabel>Status</InputLabel>
                  <Select
                    {...register('status')}
                    label="Status"
                    defaultValue={selectedVisit.status || 'Schemalagd'}
                  >
                    <MenuItem value="Schemalagd">Schemalagd</MenuItem>
                    <MenuItem value="Pågående">Pågående</MenuItem>
                    <MenuItem value="Slutförd">Slutförd</MenuItem>
                    <MenuItem value="Inställd">Inställd</MenuItem>
                  </Select>
                </FormControl>

                <TextField
                  fullWidth
                  label="Anteckningar"
                  margin="normal"
                  multiline
                  rows={3}
                  {...register('notes')}
                  helperText="Frivilliga anteckningar om besöket"
                />

                <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
                  <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    disabled={loading}
                    sx={{ flex: 1 }}
                  >
                    {loading ? <CircularProgress size={24} /> : 'Uppdatera Bokning'}
                  </Button>
                  <Button
                    variant="outlined"
                    onClick={() => navigate('/')}
                    sx={{ flex: 1 }}
                  >
                    Avbryt
                  </Button>
                </Box>
              </Box>
            )}
          </Box>
        </Paper>
      </Container>
    </LocalizationProvider>
  );
};

export default UppdateraBokning;
