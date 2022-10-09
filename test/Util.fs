module Fable.Python.Tests.Util

module Testing =
#if FABLE_COMPILER
    open Fable.Core
    open Fable.Core.PyInterop
    open Fable.Python.XUnit

    type Assert =
        [<Emit("assert $0 == $1")>]
        static member AreEqual(actual: 'T, expected: 'T, ?msg: string) : unit = nativeOnly

        [<Emit("assert not $0 == $1")>]
        static member NotEqual(actual: 'T, expected: 'T, ?msg: string) : unit = nativeOnly

    let equal expected actual : unit = Assert.AreEqual(actual, expected)
    let notEqual expected actual : unit = Assert.NotEqual(actual, expected)

    type FactAttribute = Fable.Python.XUnit.FactAttribute

#else
    open Xunit
    type FactAttribute = Xunit.FactAttribute

    let equal<'T> (expected: 'T) (actual: 'T) : unit = Assert.Equal(expected, actual)
    let notEqual<'T> (expected: 'T) (actual: 'T) : unit = Assert.NotEqual(expected, actual)
    Assert.True(true)
#endif
    let rec sumFirstSeq (zs: seq<float>) (n: int) : float =
        match n with
        | 0 -> 0.
        | 1 -> Seq.head zs
        | _ ->
            (Seq.head zs)
            + sumFirstSeq (Seq.skip 1 zs) (n - 1)
