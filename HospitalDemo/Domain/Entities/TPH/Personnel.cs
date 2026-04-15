using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.TPH;

public abstract class Personnel : AuditableEntity
{
    public Department Department { get; private set; } = null!;
    public int DepartmentId { get; private set; }

    private DateTime _shiftStart;
    private DateTime _shiftEnd;
    public DateTime ShiftStart => _shiftStart;
    public DateTime ShiftEnd => _shiftEnd;
    public Gender Gender { get; private set; }

    private PhoneNumber _phoneNumber = null!;
    private EmailAddress _emailAddress = null!;

    public PhoneNumber PhoneNumber => _phoneNumber;
    public EmailAddress EmailAddress => _emailAddress;

    protected Personnel(
        int departmentId,
        Gender gender,
        DateTime shiftStart,
        DateTime shiftEnd,
        PhoneNumber phoneNumber,
        EmailAddress emailAddress)
    {
        AssignDepartment(departmentId);
        UpdateGender(gender);
        SetShift(shiftStart, shiftEnd);
        UpdatePhoneNumber(phoneNumber);
        UpdateMailAddress(emailAddress);
    }

    protected Personnel () { }

    public void AssignDepartment(int departmentId)
    {
        departmentId.CheckIfZero();
        DepartmentId = departmentId;
    }

    public void UpdateGender(Gender gender)
    {
        Gender = gender;
    }

    public void SetShift(DateTime startDate, DateTime endDate)
    {
        startDate.CheckStartHigherThanEnd(endDate);

        _shiftStart = startDate;
        _shiftEnd = endDate;
    }

    public void UpdatePhoneNumber(PhoneNumber number)
    {
        number.CheckNull();
        
        if (_phoneNumber.Equals(number))
            throw new ArgumentException("Phone number is already in use");

        _phoneNumber = number;
    }

    public void UpdateMailAddress(EmailAddress email)
    {
        _emailAddress = email ?? throw new ArgumentException(nameof(email));
    }
}