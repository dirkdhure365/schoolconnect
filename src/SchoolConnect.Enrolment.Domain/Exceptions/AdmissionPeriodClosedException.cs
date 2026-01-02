namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class AdmissionPeriodClosedException : Exception
{
    public AdmissionPeriodClosedException(Guid admissionPeriodId)
        : base($"Admission period with ID '{admissionPeriodId}' is closed and cannot accept applications.")
    {
    }

    public AdmissionPeriodClosedException(Guid admissionPeriodId, string message)
        : base(message)
    {
    }
}
