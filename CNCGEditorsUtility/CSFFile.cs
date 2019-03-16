using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using CNCGEditors.IO;
using CNCGEditors.Properties;

namespace CNCGEditors {
    [Serializable]
    public class CSFFile : ICloneable, ISerializable {
        #region Constants
        public const string SerializationNameLabels = "Labels",
                            SerializationNameLanguage = "Language",
                            SerializationNameType = "Type",
                            SerializationNameUnused = "Unused",
                            SerializationNameVersion = "Version";
        private const string CSF = " FSC";
        #endregion

        #region Fields
        private static readonly char[] CSFCharArray;
        private IList<CSFLabel> m_labels;
        private Languages m_language;
        private Types m_type;
        private uint m_unused;
        private Versions m_version;
        #endregion

        #region Enums
        public enum Languages : uint {
            EnglishUS = 0,
            EnglishUK = 1,
            German = 2,
            French = 3,
            Spanish = 4,
            Italian = 5,
            Japanese = 6,
            Jabberwockie = 7,
            Korean = 8,
            Chinese = 9
        }
        public enum Types {
            CSF
        }
        public enum Versions : uint {
            Nox = 2,
            RA2YRGeneralsZHBFME = 3
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct Header {
            #region Fields
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Type;
            public Versions Version;
            public int LabelsCount;
            public int StringsCount;
            public uint Unused;
            public Languages Language;
            #endregion

            #region Constructors
            public Header(char[] type, Versions version, int labelsCount, int stringsCount, uint unused, Languages language)
            : this() {
                this.Type = type;
                this.Version = version;
                this.LabelsCount = labelsCount;
                this.StringsCount = stringsCount;
                this.Unused = unused;
                this.Language = language;
            }
            #endregion
        }
        #endregion

        #region Constructors
        static CSFFile() {
            CSFCharArray = CSF.ToCharArray();
        }
        public CSFFile(Stream stream) {
            Header header;

            if (stream == null) {
                throw new ArgumentNullException(Resources.Stream);
            }
            header = stream.ReadStructure<Header>();
            switch (new string(header.Type)) {
                case CSF:
                    this.m_type = Types.CSF;
                    break;
                default:
                    throw new InvalidDataException(string.Format(Resources.Invalid, Resources.CSFFile, Resources.Type));
            }
            this.m_version = header.Version;
            this.m_unused = header.Unused;
            this.m_language = header.Language;
            this.m_labels = new List<CSFLabel>(header.LabelsCount);
            for (int i = 0; i < header.LabelsCount; i++) {
                this.m_labels.Add(this.IsReadOnly ? new ReadonlyCSFLabel(stream) : new CSFLabel(stream));
            }
        }
        public CSFFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : this(
        (Types)serializationInfo.GetValue(SerializationNameType, typeof(Types)),
        (Versions)serializationInfo.GetValue(SerializationNameVersion, typeof(Versions)),
        serializationInfo.GetUInt32(SerializationNameUnused),
        (Languages)serializationInfo.GetValue(SerializationNameLanguage, typeof(Languages)),
        (IList<CSFLabel>)serializationInfo.GetValue(SerializationNameLabels, typeof(IList<CSFLabel>))) {
        }
        public CSFFile(Types type, Versions version, uint unused, Languages language, IEnumerable<CSFLabel> labels)
        : this(type, version, unused, language, new List<CSFLabel>(labels)) {
        }
        public CSFFile(Types type, Versions version, uint unused, Languages language, IList<CSFLabel> labels) {
            if (type != Types.CSF) {
                throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.CSFFile, Resources.Type));
            }
            if (labels == null) {
                throw new ArgumentNullException("labels");
            }
            if (this.IsReadOnly) {
                for (int i = 0; i < labels.Count; i++) {
                    labels[i] = labels[i].AsReadOnly();
                }
                if (!labels.IsReadOnly) {
                    labels = new ReadOnlyCollection<CSFLabel>(labels);
                }
            }
            else if (labels.IsReadOnly) {
                throw new ArgumentException(Resources.ListMustNoBeReadOnly, Resources.Value);
            }
            this.m_labels = labels;
            this.m_language = language;
            this.m_type = type;
            this.m_unused = unused;
            this.m_version = version;
        }
        #endregion

        #region Methods
        public virtual CSFFile AsReadOnly() {
            return new ReadonlyCSFFile(this.Type, this.Version, this.Unused, this.Language, this.Labels);
        }
        public virtual object Clone() {
            return this.Clone(this.IsReadOnly);
        }
        public virtual object Clone(bool readOnly) {
            CSFLabel[] labels;

            labels = new CSFLabel[this.Labels.Count];
            for (int i = 0; i < this.Labels.Count; i++) {
                labels[i] = (CSFLabel)this.Labels[i].Clone(readOnly);
            }
            return readOnly ? new ReadonlyCSFFile(this.Type, this.Version, this.Unused, this.Language, labels) : new CSFFile(this.Type, this.Version, this.Unused, this.Language, labels);
        }
        public virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext) {
            serializationInfo.AddValue(SerializationNameType, this.Type, typeof(Types));
            serializationInfo.AddValue(SerializationNameVersion, this.Version, typeof(Versions));
            serializationInfo.AddValue(SerializationNameUnused, this.Unused);
            serializationInfo.AddValue(SerializationNameLanguage, this.Language, typeof(Languages));
            serializationInfo.AddValue(SerializationNameLabels, this.Labels, typeof(IList<CSFLabel>));
        }
        public void WriteToStream(Stream stream) {
            char[] typeCharArray;

            switch (this.m_type) {
                case Types.CSF:
                    typeCharArray = CSFCharArray;
                    break;
                default:
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.CSFFile, Resources.Type));
            }
            {
                long streamStartPos;

                streamStartPos = stream.Position;
                {
                    long streamPosHeader;

                    streamPosHeader = streamStartPos + Marshal.SizeOf(typeof(Header));
                    if (stream.Length < streamPosHeader) {
                        stream.SetLength(streamPosHeader);
                    }
                    stream.Position = streamPosHeader;
                }
                {
                    int labelsCount,
                        stringsCount;

                    stringsCount = 0;
                    labelsCount = this.Labels.Count;
                    for (int i = 0; i < labelsCount; i++) {
                        CSFLabel label;

                        label = this.Labels[i];
                        stringsCount += label.Strings.Count;
                        label.WriteToStream(stream);
                    }
                    {
                        long streamEndPos;

                        streamEndPos = stream.Position;
                        stream.Position = streamStartPos;
                        stream.WriteStructure(new Header(typeCharArray, this.Version, labelsCount, stringsCount, this.Unused, this.Language));
                        stream.Position = streamEndPos;
                    }
                }
            }
        }
        #endregion

        #region Properties
        public virtual bool IsReadOnly {
            get {
                return false;
            }
        }
        public virtual IList<CSFLabel> Labels {
            get {
                return this.m_labels;
            }
            set {
                if (value == null) {
                    throw new ArgumentNullException(Resources.Value);
                }
                if (value.IsReadOnly) {
                    throw new ArgumentException(Resources.ListMustNoBeReadOnly, Resources.Value);
                }
                this.m_labels = value;
            }
        }
        public virtual Languages Language {
            get {
                return this.m_language;
            }
            set {
                this.m_language = value;
            }
        }
        public virtual Types Type {
            get {
                return this.m_type;
            }
            set {
                if (value != Types.CSF) {
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.CSFFile, Resources.Type));
                }
                this.m_type = value;
            }
        }
        public virtual uint Unused {
            get {
                return this.m_unused;
            }
            set {
                this.m_unused = value;
            }
        }
        public virtual Versions Version {
            get {
                return this.m_version;
            }
            set {
                this.m_version = value;
            }
        }
        public int StringsCount {
            get {
                return this.Labels.Sum(label => label.Strings.Count);
            }
        }
        #endregion
    }
    [Serializable]
    public class CSFLabel : ICloneable, ISerializable {
        #region Constants
        public const string SerializationNameName = "Name",
                            SerializationNameStrings = "Strings",
                            SerializationNameType = "Type";
        private const string LBL = " LBL";
        #endregion

        #region Fields
        private static readonly char[] LBLCharArray;
        private string m_name;
        private IList<CSFString> m_strings;
        private Types m_type;
        #endregion

        #region Enums
        public enum Types {
            LBL
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct Header {
            #region Fields
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Type;
            public int StringsCount;
            public int NameLength;
            #endregion

            #region Constructors
            public Header(char[] type, int stringsCount, int nameLength)
            : this() {
                this.Type = type;
                this.StringsCount = stringsCount;
                this.NameLength = nameLength;
            }
            #endregion
        }
        #endregion

        #region Constructors
        static CSFLabel() {
            LBLCharArray = LBL.ToCharArray();
        }
        public CSFLabel(Stream stream) {
            Header header;

            if (stream == null) {
                throw new ArgumentNullException(Resources.Stream);
            }
            header = stream.ReadStructure<Header>();
            switch (new string(header.Type)) {
                case LBL:
                    this.m_type = Types.LBL;
                    break;
                default:
                    throw new InvalidDataException(string.Format(Resources.Invalid, Resources.Label, Resources.Type));
            }
            {
                string name;

                name = Encoding.Default.GetString(stream.Read(header.NameLength));
                if (HasNameInvalidCharacters(name)) {
                    throw new InvalidDataException(string.Format(Resources.Invalid, Resources.Name, Resources.Characters));
                }
                this.m_name = name;
            }
            this.m_strings = new List<CSFString>(header.StringsCount);
            for (int i = 0; i < header.StringsCount; i++) {
                this.m_strings.Add(this.IsReadOnly ? new ReadonlyCSFString(stream) : new CSFString(stream));
            }
        }
        public CSFLabel(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : this(
        (Types)serializationInfo.GetValue(SerializationNameType, typeof(Types)),
        serializationInfo.GetString(SerializationNameName),
        (IList<CSFString>)serializationInfo.GetValue(SerializationNameStrings, typeof(IList<CSFString>))) {
        }
        public CSFLabel(Types type, string name, IEnumerable<CSFString> strings)
        : this(type, name, new List<CSFString>(strings)) {
        }
        public CSFLabel(Types type, string name, IList<CSFString> strings) {
            if (type != Types.LBL) {
                throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.Label, Resources.Type));
            }
            if (name == null) {
                throw new ArgumentNullException("name");
            }
            if (name == string.Empty) {
                throw new InvalidOperationException("A name of a label have at least one character.");
            }
            if (HasNameInvalidCharacters(name)) {
                throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.Name, Resources.Characters));
            }
            if (strings == null) {
                throw new ArgumentNullException("strings");
            }
            if (this.IsReadOnly) {
                for (int i = 0; i < strings.Count; i++) {
                    strings[i] = strings[i].AsReadOnly();
                }
                if (!strings.IsReadOnly) {
                    strings = new ReadOnlyCollection<CSFString>(strings);
                }
            }
            else if (strings.IsReadOnly) {
                throw new ArgumentException(Resources.ListMustNoBeReadOnly, Resources.Value);
            }
            this.m_name = name;
            this.m_strings = strings;
            this.m_type = type;
        }
        #endregion

        #region Methods
        public virtual CSFLabel AsReadOnly() {
            return new ReadonlyCSFLabel(this.Type, this.Name, this.Strings);
        }
        public virtual object Clone() {
            return this.Clone(this.IsReadOnly);
        }
        public virtual object Clone(bool readOnly) {
            CSFString[] strings;

            strings = new CSFString[this.Strings.Count];
            for (int i = 0; i < this.Strings.Count; i++) {
                strings[i] = (CSFString)this.Strings[i].Clone(readOnly);
            }
            return readOnly ? new ReadonlyCSFLabel(this.Type, new string(this.Name.ToCharArray()), strings) : new CSFLabel(this.Type, new string(this.Name.ToCharArray()), strings);
        }
        public virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext) {
            serializationInfo.AddValue(SerializationNameType, this.Type, typeof(Types));
            serializationInfo.AddValue(SerializationNameName, this.Name, typeof(string));
            serializationInfo.AddValue(SerializationNameStrings, this.Strings, typeof(IList<CSFString>));
        }
        public void WriteToStream(Stream stream) {
            {
                char[] typeCharArray;

                switch (this.m_type) {
                    case Types.LBL:
                        typeCharArray = LBLCharArray;
                        break;
                    default:
                        throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.Label, Resources.Type));
                }
                stream.WriteStructure(new Header(typeCharArray, this.Strings.Count, this.m_name.Length));
            }
            stream.Write(Encoding.Default.GetBytes(this.m_name));
            {
                int stringsCount;

                stringsCount = this.Strings.Count;
                for (int i = 0; i < stringsCount; i++) {
                    this.Strings[i].WriteToStream(stream);
                }
            }
        }
        private static bool HasNameInvalidCharacters(string name) {
            int nameLength;

            nameLength = name.Length;
            for (int i = 0; i < nameLength; i++) {
                int valueCharCode;

                valueCharCode = name[i];
                if (valueCharCode <= 0x20 || valueCharCode > 0xFF) {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Properties
        public virtual bool IsReadOnly {
            get {
                return false;
            }
        }
        public virtual string Name {
            get {
                return this.m_name;
            }
            set {
                if (value == null) {
                    throw new ArgumentNullException(Resources.Value);
                }
                if (value == string.Empty) {
                    throw new InvalidOperationException("A name of a label have at least one character.");
                }
                if (HasNameInvalidCharacters(value)) {
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.Name, Resources.Characters));
                }
                this.m_name = value;
            }
        }
        public virtual IList<CSFString> Strings {
            get {
                return this.m_strings;
            }
            set {
                if (value == null) {
                    throw new ArgumentNullException(Resources.Value);
                }
                if (value.IsReadOnly) {
                    throw new ArgumentException(Resources.ListMustNoBeReadOnly, Resources.Value);
                }
                this.m_strings = value;
            }
        }
        public virtual Types Type {
            get {
                return this.m_type;
            }
            set {
                if (value != Types.LBL) {
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.Label, Resources.Type));
                }
                this.m_type = value;
            }
        }
        #endregion
    }
    [Serializable]
    public class CSFString : ICloneable, ISerializable {
        #region Constants
        public const string SerializationNameExtraValue = "ExtraValue",
                            SerializationNameType = "Type",
                            SerializationNameValue = "Value";
        private const string STR = " RTS",
                             STRW = "WRTS";
        #endregion

        #region Fields
        private static readonly char[] STRCharArray,
                                       STRWCharArray;
        private string m_extraValue,
                       m_value;
        private Types m_type;
        #endregion

        #region Enums
        public enum Types {
            STR,
            STRW
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct Header {
            #region Fields
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Type;
            public int DecodedValueCharsCount;
            #endregion

            #region Constructors
            public Header(char[] type, int decodedValueCharsCount)
            : this() {
                this.Type = type;
                this.DecodedValueCharsCount = decodedValueCharsCount;
            }
            #endregion
        }
        #endregion

        #region Constructors
        static CSFString() {
            STRCharArray = STR.ToCharArray();
            STRWCharArray = STRW.ToCharArray();
        }
        public CSFString(Stream stream) {
            Header header;

            if (stream == null) {
                throw new ArgumentNullException(Resources.Stream);
            }
            header = stream.ReadStructure<Header>();
            switch (new string(header.Type)) {
                case STR:
                    this.m_type = Types.STR;
                    break;
                case STRW:
                    this.m_type = Types.STRW;
                    break;
                default:
                    throw new InvalidDataException(string.Format(Resources.Invalid, Resources.String, Resources.Type));
            }
            this.m_value = Encoding.Unicode.GetString(Arrays.InvertBytesInByteArray(stream.Read(checked((header.DecodedValueCharsCount * 2)))));
            if (this.m_type == Types.STRW) {
                this.m_extraValue = Encoding.Default.GetString(stream.Read(BitConverter.ToInt32(stream.Read(sizeof(int)), 0)));
            }
        }
        public CSFString(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : this(
        (Types)serializationInfo.GetValue(SerializationNameType, typeof(Types)),
        serializationInfo.GetString(SerializationNameValue),
        serializationInfo.GetString(SerializationNameExtraValue)) {
        }
        public CSFString(Types type, string value, string extraValue) {
            if (type != Types.STR && type != Types.STRW) {
                throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.String, Resources.Type));
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            if (type == Types.STRW && extraValue == null) {
                throw new ArgumentNullException("extraValue");
            }
            this.m_extraValue = value;
            this.m_type = type;
            this.m_value = value;
        }
        #endregion

        #region Methods
        public virtual CSFString AsReadOnly() {
            return new ReadonlyCSFString(this.Type, this.Value, this.ExtraValue);
        }
        public virtual void ChangeType(Types type, string extraValue) {
            if (this.Type == type) {
                return;
            }
            switch (type) {
                case Types.STR:
                    this.Type = Types.STR;
                    break;
                case Types.STRW:
                    if (extraValue == null && this.ExtraValue == null) {
                        throw new InvalidOperationException("There is no value to set 'ExtraValue' since both 'extraValue' and 'ExtraValue' are null.");
                    }
                    this.ExtraValue = extraValue ?? this.ExtraValue;
                    break;
                default:
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.String, Resources.Type));
            }
        }
        public virtual object Clone() {
            return this.Clone(this.IsReadOnly);
        }
        public virtual object Clone(bool readOnly) {
            return readOnly
                   ? new ReadonlyCSFString(this.Type, new string(this.Value.ToCharArray()), new string(this.ExtraValue.ToCharArray()))
                   : new CSFString(this.Type, new string(this.Value.ToCharArray()), new string(this.ExtraValue.ToCharArray()));
        }
        public virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext) {
            serializationInfo.AddValue(SerializationNameType, this.Type, typeof(Types));
            serializationInfo.AddValue(SerializationNameValue, this.Value, typeof(string));
            serializationInfo.AddValue(SerializationNameExtraValue, this.ExtraValue, typeof(string));
        }
        public void WriteToStream(Stream stream) {
            {
                char[] typeCharArray;

                switch (this.m_type) {
                    case Types.STR:
                        typeCharArray = STRCharArray;
                        break;
                    case Types.STRW:
                        typeCharArray = STRWCharArray;
                        break;
                    default:
                        throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.String, Resources.Type));
                }
                stream.WriteStructure(new Header(typeCharArray, this.m_value.Length));
            }
            stream.Write(Arrays.InvertBytesInByteArray(Encoding.Unicode.GetBytes(this.m_value)));
            if (this.m_type == Types.STRW) {
                stream.Write(BitConverter.GetBytes(this.m_extraValue.Length));
                stream.Write(Encoding.Default.GetBytes(this.m_extraValue));
            }
        }
        #endregion

        #region Properties
        public virtual string ExtraValue {
            get {
                return this.m_extraValue;
            }
            set {
                if (value == null && this.Type == Types.STRW) {
                    throw new ArgumentNullException(Resources.Value);
                }
                this.m_extraValue = value;
            }
        }
        public virtual bool IsReadOnly {
            get {
                return false;
            }
        }
        public virtual Types Type {
            get {
                return this.m_type;
            }
            protected set {
                if (value != Types.STR && value != Types.STRW) {
                    throw new InvalidOperationException(string.Format(Resources.Invalid, Resources.String, Resources.Type));
                }
                this.m_type = value;
            }
        }
        public virtual string Value {
            get {
                return this.m_value;
            }
            set {
                if (value == null) {
                    throw new ArgumentNullException(Resources.Value);
                }
                this.m_value = value;
            }
        }
        #endregion
    }
    [Serializable]
    public sealed class ReadonlyCSFFile : CSFFile {
        #region Constructors
        public ReadonlyCSFFile(Stream stream)
        : base(stream) {
        }
        public ReadonlyCSFFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext) {
        }
        public ReadonlyCSFFile(Types type, Versions version, uint unused, Languages language, IEnumerable<CSFLabel> labels)
        : base(type, version, unused, language, labels) {
        }
        public ReadonlyCSFFile(Types type, Versions version, uint unused, Languages language, IList<CSFLabel> labels)
        : base(type, version, unused, language, labels) {
        }
        #endregion

        #region Methods
        public override CSFFile AsReadOnly() {
            return this;
        }
        public override object Clone(bool readOnly) {
            if (readOnly) {
                return base.Clone(true);
            }
            throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
        }
        #endregion

        #region Properties
        public override bool IsReadOnly {
            get {
                return true;
            }
        }
        public override IList<CSFLabel> Labels {
            get {
                return base.Labels;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.CSFFile));
            }
        }
        public override Languages Language {
            get {
                return base.Language;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.CSFFile));
            }
        }
        public override Types Type {
            get {
                return base.Type;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.CSFFile));
            }
        }
        public override uint Unused {
            get {
                return base.Unused;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.CSFFile));
            }
        }
        public override Versions Version {
            get {
                return base.Version;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.CSFFile));
            }
        }
        #endregion
    }
    [Serializable]
    public sealed class ReadonlyCSFLabel : CSFLabel {
        #region Constructors
        public ReadonlyCSFLabel(Stream stream)
        : base(stream) {
        }
        public ReadonlyCSFLabel(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext) {
        }
        public ReadonlyCSFLabel(Types type, string name, IEnumerable<CSFString> strings)
        : base(type, name, strings) {
        }
        public ReadonlyCSFLabel(Types type, string name, IList<CSFString> strings)
        : base(type, name, strings) {
        }
        #endregion

        #region Methods
        public override CSFLabel AsReadOnly() {
            return this;
        }
        public override object Clone(bool readOnly) {
            if (readOnly) {
                return base.Clone(true);
            }
            throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
        }
        #endregion

        #region Properties
        public override bool IsReadOnly {
            get {
                return true;
            }
        }
        public override string Name {
            get {
                return base.Name;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.Label));
            }
        }
        public override IList<CSFString> Strings {
            get {
                return base.Strings;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.Label));
            }
        }
        public override Types Type {
            get {
                return base.Type;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.Label));
            }
        }
        #endregion
    }
    [Serializable]
    public sealed class ReadonlyCSFString : CSFString {
        #region Constructors
        public ReadonlyCSFString(Stream stream)
        : base(stream) {
        }
        public ReadonlyCSFString(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext) {
        }
        public ReadonlyCSFString(Types type, string value, string extraValue)
        : base(type, value, extraValue) {
        }
        #endregion

        #region Methods
        public override CSFString AsReadOnly() {
            return this;
        }
        public override void ChangeType(Types type, string extraValue) {
            throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
        }
        public override object Clone(bool readOnly) {
            if (readOnly) {
                return base.Clone(true);
            }
            throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
        }
        #endregion

        #region Properties
        public override string ExtraValue {
            get {
                return base.ExtraValue;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
            }
        }
        public override bool IsReadOnly {
            get {
                return true;
            }
        }
        public override string Value {
            get {
                return base.Value;
            }
            set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
            }
        }
        public override Types Type {
            get {
                return base.Type;
            }
            protected set {
                throw new NotSupportedException(string.Format(Resources.ReadOnly, Resources.String));
            }
        }
        #endregion
    }
}