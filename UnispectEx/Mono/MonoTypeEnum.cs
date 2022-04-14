namespace UnispectEx.Mono {
	// https://github.com/mono/mono/blob/main/mono/metadata/blob.h#L12
	public enum MonoTypeEnum {
		MonoTypeEnd = 0x00, /* End of List */
		MonoTypeVoid = 0x01,
		MonoTypeBoolean = 0x02,
		MonoTypeChar = 0x03,
		MonoTypeI1 = 0x04,
		MonoTypeU1 = 0x05,
		MonoTypeI2 = 0x06,
		MonoTypeU2 = 0x07,
		MonoTypeI4 = 0x08,
		MonoTypeU4 = 0x09,
		MonoTypeI8 = 0x0a,
		MonoTypeU8 = 0x0b,
		MonoTypeR4 = 0x0c,
		MonoTypeR8 = 0x0d,
		MonoTypeString = 0x0e,
		MonoTypePtr = 0x0f, /* arg: <type> token */
		MonoTypeByref = 0x10, /* arg: <type> token */
		MonoTypeValuetype = 0x11, /* arg: <type> token */
		MonoTypeClass = 0x12, /* arg: <type> token */
		MonoTypeVar = 0x13, /* number */
		MonoTypeArray = 0x14, /* type, rank, boundsCount, bound1, loCount, lo1 */
		MonoTypeGenericinst = 0x15, /* <type> <type-arg-count> <type-1> \x{2026} <type-n> */
		MonoTypeTypedbyref = 0x16,
		MonoTypeI = 0x18,
		MonoTypeU = 0x19,
		MonoTypeFnptr = 0x1b, /* arg: full method signature */
		MonoTypeObject = 0x1c,
		MonoTypeSzarray = 0x1d, /* 0-based one-dim-array */
		MonoTypeMvar = 0x1e, /* number */
		MonoTypeCmodReqd = 0x1f, /* arg: typedef or typeref token */
		MonoTypeCmodOpt = 0x20, /* optional arg: typedef or typref token */
		MonoTypeInternal = 0x21, /* CLR internal type */

		MonoTypeModifier = 0x40, /* Or with the following types */
		MonoTypeSentinel = 0x41, /* Sentinel for varargs method signature */
		MonoTypePinned = 0x45, /* Local var that points to pinned object */

		MonoTypeEnum = 0x55 /* an enumeration */
	}
}